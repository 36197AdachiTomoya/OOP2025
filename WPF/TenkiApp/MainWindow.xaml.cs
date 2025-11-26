using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace WeatherApp {
    public partial class MainWindow : Window {
        private readonly HttpClient _http = new HttpClient();
        private bool _webViewInitialized = false;

        public MainWindow() {
            InitializeComponent();
            InitializeWebView2();
        }

        // WebView2 初期化処理
        private async void InitializeWebView2() {
            try {
                // WebView2の初期化を待つ
                await MapBrowser.EnsureCoreWebView2Async();

                // WebView2のナビゲーションが完了した時に呼ばれるイベント
                MapBrowser.CoreWebView2.NavigationCompleted += (sender, args) => {
                    if (args.IsSuccess) {
                        // 正常に読み込まれた場合
                        _webViewInitialized = true;
                    } else {
                        // エラーが発生した場合
                        MessageBox.Show($"WebView2の読み込みに失敗しました: {args.WebErrorStatus}");
                    }
                };

                // 地図を表示するHTMLを設定
                string html = @"
<!DOCTYPE html>
<html lang='ja'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>日本地図 - 天気予報アプリ</title>
    <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>
    <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
    <style>
        #map { height: 100%; width: 100%; margin: 0; padding: 0; }
    </style>
</head>
<body>
    <h1>日本地図</h1>
    <div id='map'></div>
    <script>
        var map = L.map('map').setView([36.2048, 138.2529], 5);  // 日本の中心付近

        // OpenStreetMapのタイルを地図に追加
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href=""https://www.openstreetmap.org/copyright"">OpenStreetMap</a> contributors'
        }).addTo(map);

        // 日本の主要都市にマーカーを追加
        var tokyo = L.marker([35.6762, 139.6503]).addTo(map)
            .bindPopup('<b>東京</b><br>日本の首都').openPopup();
        var osaka = L.marker([34.6937, 135.5023]).addTo(map)
            .bindPopup('<b>大阪</b><br>商業の中心地');
        var sapporo = L.marker([43.0618, 141.3545]).addTo(map)
            .bindPopup('<b>札幌</b><br>北海道の中心');
    </script>
</body>
</html>";

                // WebView2にHTMLを表示
                MapBrowser.NavigateToString(html);
            }
            catch (Exception ex) {
                MessageBox.Show($"WebView2の初期化に失敗しました: {ex.Message}");
            }
        }

        // 地名から天気情報を取得
        private async void FetchButton_Click(object sender, RoutedEventArgs e) {
            string place = LocationTextBox.Text.Trim();
            if (string.IsNullOrEmpty(place)) {
                MessageBox.Show("場所を入力してください");
                return;
            }

            var coords = await GetLatLon(place);
            if (coords == null) {
                MessageBox.Show("場所が見つかりません");
                return;
            }

            double lat = coords.Value.lat;
            double lon = coords.Value.lon;
            string name = coords.Value.displayName;
            LocationNameText.Text = $"場所: {name}";

            await UpdateWeather(lat, lon);

            if (_webViewInitialized) {
                AddMarkerToMap(lat, lon, name);
            } else {
                MessageBox.Show("地図の初期化が完了していません。再試行してください。");
            }
        }

        // 緯度経度を取得するメソッド
        private async Task<(double lat, double lon, string displayName)?> GetLatLon(string placeName) {
            string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(placeName)}&format=json&limit=1";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("User-Agent", "WeatherApp");
            var resp = await _http.SendAsync(req);
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var arr = doc.RootElement;
            if (arr.ValueKind != JsonValueKind.Array || arr.GetArrayLength() == 0) return null;

            var first = arr[0];
            double lat = double.Parse(first.GetProperty("lat").GetString(), CultureInfo.InvariantCulture);
            double lon = double.Parse(first.GetProperty("lon").GetString(), CultureInfo.InvariantCulture);
            string displayName = first.GetProperty("display_name").GetString();
            return (lat, lon, displayName);
        }

        // 天気情報を更新
        private async Task UpdateWeather(double lat, double lon) {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=Asia/Tokyo";
            var json = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("current_weather", out var current)) {
                double temp = current.GetProperty("temperature").GetDouble();
                double wind = current.GetProperty("windspeed").GetDouble();
                TempText.Text = $"{temp}°C";
                WindText.Text = $"風速: {wind} m/s";
            }
        }

        // 地図にマーカーを追加
        private void AddMarkerToMap(double lat, double lon, string name) {
            MapBrowser.CoreWebView2.ExecuteScriptAsync(
                $"addMarker({lat.ToString(CultureInfo.InvariantCulture)}, {lon.ToString(CultureInfo.InvariantCulture)}, '{name}');"
            );
        }

        // プレースホルダー設定のイベント
        private void LocationTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (LocationTextBox.Text == "場所を入力") {
                LocationTextBox.Text = "";
                LocationTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void LocationTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(LocationTextBox.Text)) {
                LocationTextBox.Text = "場所を入力";
                LocationTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
