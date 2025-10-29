using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomerApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ObservableCollection<Customer> _customer = new ObservableCollection<Customer>();
        public MainWindow() {
            InitializeComponent();
            CustomerListView.ItemsSource = _customer;
            CustomerListView.SelectionChanged += CustomerListView_SelectionChanged;

            CreateCustomerTable();

            RefreshCustomerList();

        }

        private void CreateCustomerTable() {
            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>(); // 顧客テーブルがなければ作成
            }
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedCustomer = CustomerListView.SelectedItem as Customer;
            if (selectedCustomer != null) {
                NameTextBox.Text = selectedCustomer.Name;
                PhoneTextBox.Text = selectedCustomer.Phone;
                AddressTextBox.Text = selectedCustomer.Address;

                if (selectedCustomer.Image != null) {
                    using (var ms = new MemoryStream(selectedCustomer.Image)) {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        CustomerImage.Source = bitmap;
                    }
                } else {
                    CustomerImage.Source = null;  // 画像なしの場合はクリア
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            byte[] imageBytes = null;

            // CustomerImage.Source から byte[] に変換（画像がある場合）
            if (CustomerImage.Source is BitmapImage bitmapImage) {
                using (MemoryStream ms = new MemoryStream()) {
                    BitmapEncoder encoder = new PngBitmapEncoder(); // PNG形式で保存
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(ms);
                    imageBytes = ms.ToArray();
                }
            }

            var customer = new Customer() {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Image = imageBytes,
            };

            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();
                connection.Insert(customer);
            }

            // 保存後に一覧を更新する
            RefreshCustomerList();
        }

        // 顧客一覧を読み込み、_customerコレクションを更新するメソッド
        private void RefreshCustomerList() {
            using (var connection = new SQLiteConnection(App.databasePath)) {
                var customers = connection.Table<Customer>().ToList();

                _customer.Clear();
                foreach (var customer in customers) {
                    _customer.Add(customer);
                }
            }
        }


        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            var selectedCustomer = CustomerListView.SelectedItem as Customer;
            if (selectedCustomer == null) {
                MessageBox.Show("更新する顧客を選択してください。");
                return;
            }

            byte[] imageBytes = null;

            if (CustomerImage.Source is BitmapImage bitmapImage) {
                using (MemoryStream ms = new MemoryStream()) {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(ms);
                    imageBytes = ms.ToArray();
                }
            }

            // 選択中の顧客情報を更新
            selectedCustomer.Name = NameTextBox.Text;
            selectedCustomer.Phone = PhoneTextBox.Text;
            selectedCustomer.Address = AddressTextBox.Text;
            selectedCustomer.Image = imageBytes;

            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();
                connection.Update(selectedCustomer);
            }

            // ListViewを更新表示
            RefreshCustomerList();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var selectedCustomer = CustomerListView.SelectedItem as Customer;

            if (selectedCustomer != null) {
                try {
                    using (var connection = new SQLiteConnection(App.databasePath)) {
                        connection.CreateTable<Customer>();
                        if (connection.Delete<Customer>(selectedCustomer.Id) > 0) {
                            _customer.Remove(selectedCustomer);
                        } else {
                            MessageBox.Show("データベースに該当の顧客が見つける気がありませんでした。");
                        }
                    }
                }
                catch (Exception ex) {
                    
                }
            } else {
                
            }
        }



        private void ImageButton_Click(object sender, RoutedEventArgs e) {
            // 画像ファイルを選ぶダイアログを開く
            var dlg = new OpenFileDialog();
            dlg.Filter = "画像ファイル (*.jpg;*.png)|*.jpg;*.png";

            if (dlg.ShowDialog() == true) {
                string sourcePath = dlg.FileName;

                // 選んだ画像をImageコントロールに表示
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(sourcePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                CustomerImage.Source = bitmap;
            }
        }
    }
}