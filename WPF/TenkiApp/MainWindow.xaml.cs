using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApp {
    public partial class MainWindow : Window {
        private readonly HttpClient _http = new HttpClient();

        public MainWindow() {
            InitializeComponent();
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
            LocationNameTextRight.Text = $"場所: {name}";

            await UpdateWeather(lat, lon);
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

                // 左側の天気カードに情報を表示
                TempText.Text = $"{temp}°C";
                WindText.Text = $"風速: {wind} m/s";

                // 右側の天気情報に情報を表示
                TempTextRight.Text = $"{temp}°C";
                WindTextRight.Text = $"風速: {wind} m/s";
            }
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

    public class GeoCoordinates {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
