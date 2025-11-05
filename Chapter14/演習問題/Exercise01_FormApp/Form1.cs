using System.Security.Cryptography;

namespace Exercise01_FormApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            textBox1.Text = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "テキストファイル(*.txt)|*.txt";
            openFileDialog.Title = "ファイル選択";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string selectedFile = openFileDialog.FileName;
                //選択されたファイルを扱う処理をここに書く
                await ReadFileAsync(selectedFile);

            }     
            
             
        }

        public async Task ReadFileAsync(string selectedFile) {
            using (StreamReader reader = new StreamReader(selectedFile)) {
                string line;
                while((line = await reader.ReadLineAsync()) != null) {
                    textBox1.AppendText(line + Environment.NewLine);
                }
            }
        }
        
    }
}
