using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = GetColorList();
        }

        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            colorArea.Background = new SolidColorBrush
                        (Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value));
        }

        private void Stock_Click(object sender, RoutedEventArgs e) {
            byte r = (byte)rSlider.Value;
            byte g = (byte)gSlider.Value;
            byte b = (byte)bSlider.Value;

            string rgbColor = $"{r},{g},{b}";

            if (Record.Items.Contains(rgbColor)) {
                MessageBox.Show("すでに同じ色が登録されています。");
            } else {
                Record.Items.Add(rgbColor);
            }
        }



        private void setSliderValue(Color color) {
            rSlider.Value = color.R;
            gSlider.Value = color.G;
            bSlider.Value = color.B;

        }

        private void colorSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var comboSelectMyColor = ((ComboBox)sender).SelectedItem;
            setSliderValue(((MyColor)comboSelectMyColor).Color);
        }

        private void Record_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Record.SelectedItem is string rgbColor) {
                // 例: "255,0,0"
                var s = rgbColor.Split(',');

                if (s.Length == 3
                    && byte.TryParse(s[0], out byte r)
                    && byte.TryParse(s[1], out byte g)
                    && byte.TryParse(s[2], out byte b)) 
                    {

                    rSlider.Value = r;
                    gSlider.Value = g;
                    bSlider.Value = b;

                }
            }
        }

        private void rTextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
