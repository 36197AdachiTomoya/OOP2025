using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_ClickAsync(object sender, EventArgs e) {
            using (var wc = new HttpClient()) {
                using HttpResponseMessage response = await wc.GetAsync(cbUrl.Text);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                XDocument xdoc = XDocument.Parse(responseBody);  //RSS�̎擾

                ////RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item").Select(x =>
                    new ItemData {
                        Title = (string?)x.Element("title"),
                        Link = (string?)x.Element("link"),
                    }).ToList();


                //���X�g�{�b�N�X�փ^�C�g���\��
                items.ForEach(s => lbTitles.Items.Add(s.Title ?? "�f�[�^�Ȃ�"));


            }
        }

        //�^�C�g����I�������Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex >= 0 && lbTitles.SelectedIndex < items.Count) {
                // �C���f�b�N�X���L���Ȕ͈͂ł��邱�Ƃ��m�F
                wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);
            }
            //wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);
        }

        //�i��
        private void btForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();  // �y�[�W��i�߂�
        }

        //�߂�
        private void btBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();  // �y�[�W��߂�
        }

        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btBack.Enabled = wvRssLink.CanGoBack;
            btForward.Enabled = wvRssLink.CanGoForward;
        }



        private void btfavorite_Click(object sender, EventArgs e) {
            if (cbUrl.Text == null) {
                return;
            } else if (cbUrl.Items.Contains(cbUrl.Text)) {
                return;
            } else {
                cbUrl.Items.Add(cbUrl.Text);
            }
        }

        private void btfavoriteDelete_Click(object sender, EventArgs e) {
            cbUrl.Items.Remove(cbUrl.Text);
        }

        private void Form1_Load(object sender, EventArgs e) {
            cbUrl.Items.Add(new ItemData {Title = "�r�W�l�X", Link = "https://news.yahoo.co.jp/rss/topics/business.xml" });
            cbUrl.Items.Add(new ItemData {Title = "IT", Link = "https://news.yahoo.co.jp/rss/topics/it.xml" });
            cbUrl.Items.Add(new ItemData {Title = "����", Link = "https://news.yahoo.co.jp/rss/topics/domestic.xml" });
            cbUrl.Items.Add(new ItemData {Title = "�T�C�G���X", Link = "https://news.yahoo.co.jp/rss/topics/science.xml" });
            cbUrl.Items.Add(new ItemData {Title = "�X�|�[�c", Link = "https://news.yahoo.co.jp/rss/topics/sports.xml" });
        }
    }
}
