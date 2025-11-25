using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TenkiApp {
    public partial class MainWindow : Window {
        // HttpClient インスタンスを追加
        private readonly HttpClient _http = new HttpClient();
        private bool _webViewInitialized = false;

        public MainWindow() {
            InitializeComponent();
            InitializeWebView2();
        }

        // WebView2 初期化処理
        private async void InitializeWebView2() {
            try {
                // WebView2 の初期化を待つ
                await MapBrowser.EnsureCoreWebView2Async();

                // WebView2 初期化後にHTMLを埋め込む
                string html = @"
<!DOCTYPE html>
<html lang='ja'>
<head>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
    <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>
    <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
    <style>
        #map { height: 100%; width: 100%; margin: 0; padding: 0; }
    </style>
</head>
<body>
    <div id='map'></div>
    <script>
        var map = L.map('map').setView([36.0, 138.0], 5);  // 初期位置とズームレベル
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);

        // マーカーを追加する関数
        function addMarker(lat, lon, name) {
            var marker = L.marker([lat, lon]).addTo(map).bindPopup(name);
            map.setView([lat, lon], 10);  // マーカー追加後にズームイン
        }
    </script>
</body>
</html>";

                // HTMLをWebView2に埋め込む
                MapBrowser.NavigateToString(html);
                _webViewInitialized = true;
            }
            catch (Exception ex) {
                MessageBox.Show($"WebView2の初期化に失敗しました: {ex.Message}");
            }
        }

        // 検索ボタンを押したときの処理
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

            // WebView2 が初期化されていればマーカーを追加
            if (_webViewInitialized) {
                AddMarkerToMap(lat, lon, name);
            } else {
                MessageBox.Show("地図の初期化が完了していません。再試行してください。");
            }
        }

        // 地図にマーカーを追加するメソッド
        private void AddMarkerToMap(double lat, double lon, string name) {
            MapBrowser.CoreWebView2.ExecuteScriptAsync(
                $"addMarker({lat.ToString(CultureInfo.InvariantCulture)}, {lon.ToString(CultureInfo.InvariantCulture)}, '{name}');"
            );
        }

        // 地名から緯度経度を取得
        private async Task<(double lat, double lon, string displayName)?> GetLatLon(string placeName) {
            try {
                string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(placeName + " 日本")}&format=json&limit=1";
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("User-Agent", "TenkiApp/1.0");
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
            catch (Exception ex) {
                MessageBox.Show($"緯度経度の取得に失敗しました: {ex.Message}");
                return null;
            }
        }

        // 天気情報を更新
        private async Task UpdateWeather(double lat, double lon) {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=Asia/Tokyo";
            var json = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(json);

            // 現在の天気情報
            if (doc.RootElement.TryGetProperty("current_weather", out var current)) {
                double temp = current.GetProperty("temperature").GetDouble();
                double wind = current.GetProperty("windspeed").GetDouble();
                int weatherCode = current.GetProperty("weathercode").GetInt32();

                TempText.Text = $"{temp}°C";
                WindText.Text = $"風速: {wind} m/s";
                WeatherText.Text = GetWeatherDescription(weatherCode);
            }

            // 週間天気情報
            if (doc.RootElement.TryGetProperty("daily", out var daily)) {
                var maxTemps = daily.GetProperty("temperature_2m_max");
                var minTemps = daily.GetProperty("temperature_2m_min");
                var precipitation = daily.GetProperty("precipitation_sum");

                WeeklyWeatherStack.Children.Clear(); // 前のデータをクリア

                for (int i = 0; i < maxTemps.GetArrayLength(); i++) {
                    string date = daily.GetProperty("time")[i].GetString();
                    double maxTemp = maxTemps[i].GetDouble();
                    double minTemp = minTemps[i].GetDouble();
                    double precip = precipitation[i].GetDouble();

                    // 週間天気の表示
                    StackPanel dayPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10) };
                    TextBlock dayLabel = new TextBlock { Text = $"{date}: {maxTemp}°C / {minTemp}°C", Foreground = Brushes.White };
                    TextBlock precipLabel = new TextBlock { Text = $"降水量: {precip}mm", Foreground = Brushes.White };

                    dayPanel.Children.Add(dayLabel);
                    dayPanel.Children.Add(precipLabel);
                    WeeklyWeatherStack.Children.Add(dayPanel);
                }
            }
        }

        // 天気コードに基づく説明
        private string GetWeatherDescription(int code) => code switch {
            0 => "晴れ",
            1 => "ほぼ晴れ",
            2 => "曇り",
            3 => "曇り/小雨",
            45 => "霧",
            48 => "霧（結露）",
            51 => "霧雨（弱）",
            53 => "霧雨（中）",
            55 => "霧雨（強）",
            56 => "雪まじりの霧雨（弱）",
            57 => "雪まじりの霧雨（強）",
            61 => "小雨（弱）",
            63 => "小雨（中）",
            65 => "小雨（強）",
            66 => "雪まじりの雨（弱）",
            67 => "雪まじりの雨（強）",
            71 => "雪（弱）",
            73 => "雪（中）",
            75 => "雪（強）",
            77 => "霰",
            80 => "にわか雨（弱）",
            81 => "にわか雨（中）",
            82 => "にわか雨（強）",
            85 => "にわか雪（弱）",
            86 => "にわか雪（強）",
            95 => "雷雨",
            96 => "雷雨（軽い雹）",
            99 => "雷雨（強い雹）",
            _ => "不明"
        };
    }
}
