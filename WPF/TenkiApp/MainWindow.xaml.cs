using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private readonly HttpClient _http = new HttpClient();
        private bool _webViewInitialized = false;

        public MainWindow() {
            InitializeComponent();
            InitializeWebView2();
        }

        private async void InitializeWebView2() {
            await MapBrowser.EnsureCoreWebView2Async();
            _webViewInitialized = true;
        }

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
            LocationNameText.Text = $"場所: {coords.Value.displayName}";

            await UpdateWeather(lat, lon);

            // WebView2 が初期化されてから地図を表示
            if (_webViewInitialized)
                ShowLocationOnMap(lat, lon, coords.Value.displayName);
            else
                MapBrowser.CoreWebView2InitializationCompleted += (s, ev) => {
                    ShowLocationOnMap(lat, lon, coords.Value.displayName);
                };
        }

        private async Task<(double lat, double lon, string displayName)?> GetLatLon(string placeName) {
            string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(placeName + " 日本")}&format=json&limit=10&addressdetails=1";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "TenkiApp/1.0");
            var resp = await _http.SendAsync(request);
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var arr = doc.RootElement;
            if (arr.ValueKind != JsonValueKind.Array || arr.GetArrayLength() == 0) return null;

            foreach (var item in arr.EnumerateArray()) {
                if (!item.TryGetProperty("address", out var address)) continue;
                if (!address.TryGetProperty("country", out var country) || country.GetString() != "Japan") continue;

                if (address.TryGetProperty("city", out var _) ||
                    address.TryGetProperty("town", out var _) ||
                    address.TryGetProperty("village", out var _) ||
                    address.TryGetProperty("state", out var _)) {
                    double lat = double.Parse(item.GetProperty("lat").GetString(), CultureInfo.InvariantCulture);
                    double lon = double.Parse(item.GetProperty("lon").GetString(), CultureInfo.InvariantCulture);
                    string displayName = item.GetProperty("display_name").GetString();
                    return (lat, lon, displayName);
                }
            }

            var first = arr[0];
            double fLat = double.Parse(first.GetProperty("lat").GetString(), CultureInfo.InvariantCulture);
            double fLon = double.Parse(first.GetProperty("lon").GetString(), CultureInfo.InvariantCulture);
            string fName = first.GetProperty("display_name").GetString();
            return (fLat, fLon, fName);
        }

        private async Task UpdateWeather(double lat, double lon) {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true&daily=sunrise,sunset&timezone=Asia/Tokyo";
            var json = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("current_weather", out var current)) {
                double temp = current.GetProperty("temperature").GetDouble();
                double wind = current.GetProperty("windspeed").GetDouble();
                int weatherCode = current.GetProperty("weathercode").GetInt32();

                TempText.Text = $"{temp}°C";
                WindText.Text = $"風速: {wind} m/s";
                WeatherText.Text = GetWeatherDescription(weatherCode);
            }

            if (doc.RootElement.TryGetProperty("daily", out var daily)) {
                if (daily.TryGetProperty("sunrise", out var sunriseArr) && sunriseArr.GetArrayLength() > 0)
                    SunriseText.Text = $"日の出: {sunriseArr[0].GetString()?.Substring(11, 5)}";

                if (daily.TryGetProperty("sunset", out var sunsetArr) && sunsetArr.GetArrayLength() > 0)
                    SunsetText.Text = $"日の入り: {sunsetArr[0].GetString()?.Substring(11, 5)}";
            }
        }

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

        private void ShowLocationOnMap(double lat, double lon, string name) {
            string html = $@"
<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'/>
<link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>
<script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
<style>#map {{ height: 100%; width: 100%; margin:0; padding:0; }}</style>
</head>
<body>
<div id='map'></div>
<script>
var map = L.map('map').setView([{lat}, {lon}], 12);
L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{ maxZoom: 19 }}).addTo(map);
L.marker([{lat}, {lon}]).addTo(map).bindPopup('{name}').openPopup();
</script>
</body>
</html>";
            MapBrowser.NavigateToString(html);
        }
    }
}
