using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ConverterApp {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            string[] items = { "mm", "cm", "m", "km" };
            foreach (var item in items) {
                MetricUnit.Items.Add(item);
                ImperialUnit.Items.Add(item);
            }
        }

        private void ImperialUnitToMetric_Click(object sender, RoutedEventArgs e) {
            double result = 0;
            double i;
            string imperialUnit = ImperialUnit.SelectedItem.ToString();

            string metricUnit = MetricUnit.SelectedItem.ToString();
            if (double.TryParse(ImperialValue.Text,out i)) {
                if (ImperialUnit.Items.Equals("km")) {
                    if (metricUnit == "cm") result = i / 10;
                    else if (metricUnit == "m") result = i / 1000;
                    else if (metricUnit == "km") result = i / 1000000;
                    MetricUnit.Text = result.ToString();
                } else if (ImperialUnit.Items.Equals("m")) {
                    if (metricUnit == "cm") result = i / 10;
                    else if (metricUnit == "m") result = i / 1000;
                    else if (metricUnit == "km") result = i / 1000000;
                    MetricUnit.Text = result.ToString();
                } else if (ImperialUnit.Items.Equals("cm")) {
                    if (metricUnit == "cm") result = i / 10;
                    else if (metricUnit == "m") result = i / 1000;
                    else if (metricUnit == "km") result = i / 1000000;
                    MetricUnit.Text = result.ToString();
                } else {
                    if (metricUnit == "cm") result = i / 10;
                    else if (metricUnit == "m") result = i / 1000;
                    else if (metricUnit == "km") result = i / 1000000;
                    MetricUnit.Text = result.ToString();
                }
            } else {
                MessageBox.Show("数値を入力してください");
            }
        }
    }
}
