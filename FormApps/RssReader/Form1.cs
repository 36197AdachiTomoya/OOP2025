using System.Net;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"��v","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"�o��","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"�X�|�[�c","https://news.yahoo.co.jp/rss/topics/sports.xml" },
        };



        public Form1() {
            InitializeComponent();

        }

        private async void btRssGet_ClickAsync(object sender, EventArgs e) {
            using (var wc = new HttpClient()) {
                using HttpResponseMessage response = await wc.GetAsync(GetRssUrl(cbUrl.Text));
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

        //�R���{�{�b�N�X�̕�������`�F�b�N���ăA�N�Z�X�\��URL��ԋp����
        private string GetRssUrl(string text) {
            if (rssUrlDict.ContainsKey(text)) {
                return rssUrlDict[text];
            }
            return text;
        }

        //�^�C�g����I�������Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex >= 0 && lbTitles.SelectedIndex < items.Count) {
                // �C���f�b�N�X���L���Ȕ͈͂ł��邱�Ƃ��m�F
                wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);
            }
           
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
            bool hasSelection = lbTitles.SelectedIndex >= 0;
            btBack.Enabled = hasSelection && wvRssLink.CanGoBack;
            btForward.Enabled = hasSelection && wvRssLink.CanGoForward;
        }



        private void Form1_Load(object sender, EventArgs e) {
            cbUrl.DataSource = rssUrlDict.Select(k => k.Key).ToList();
            GoForwardBtEnableSet();
            cbUrl.Text = null;
        }

        private void btfavorite_Click(object sender, EventArgs e) {
            if (cbUrl.Text == null || tbFavoriteName.Text == null) {
                return;
            } else if (rssUrlDict.Keys.Contains(tbFavoriteName.Text)) {
                return;
            } else {
                rssUrlDict.Add(tbFavoriteName.Text,cbUrl.Text);
                cbUrl.DataSource = null;
                cbUrl.DataSource = rssUrlDict.Keys.ToList();
            }
        }

        private void btfavoriteDelete_Click(object sender, EventArgs e) {
            cbUrl.Items.Remove(cbUrl.Text);
        }

        private void tbFavoriteName_TextChanged(object sender, EventArgs e) {

        }
    }
}
