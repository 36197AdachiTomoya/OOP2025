using System.Net;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"主要","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"経済","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"スポーツ","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"科学","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"エンタメ","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"国際","https://news.yahoo.co.jp/rss/topics/world.xml" },
        };

        public Form1() {
            InitializeComponent();

        }

        private async void btRssGet_ClickAsync(object sender, EventArgs e) {
            using (var wc = new HttpClient()) {
                using HttpResponseMessage response = await wc.GetAsync(GetRssUrl(cbUrl.Text));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                XDocument xdoc = XDocument.Parse(responseBody);  //RSSの取得

                ////RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item").Select(x =>
                    new ItemData {
                        Title = (string?)x.Element("title"),
                        Link = (string?)x.Element("link"),
                    }).ToList();

                //リストボックスへタイトル表示
                items.ForEach(s => lbTitles.Items.Add(s.Title ?? "データなし"));

            }
        }

        //コンボボックスの文字列をチェックしてアクセス可能なURLを返却する
        private string GetRssUrl(string text) {
            if (rssUrlDict.ContainsKey(text)) {
                return rssUrlDict[text];
            }
            return text;
        }

        //タイトルを選択したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedIndex >= 0 && lbTitles.SelectedIndex < items.Count) {
                // インデックスが有効な範囲であることを確認
                wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);
            }

        }

        //進む
        private void btForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();  // ページを進める
        }

        //戻る
        private void btBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();  // ページを戻す
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
            if (string.IsNullOrEmpty(cbUrl.Text) || string.IsNullOrEmpty(tbFavoriteName.Text)) {
                return;
            } else if (rssUrlDict.Keys.Contains(tbFavoriteName.Text)) {
                return;
            } else {
                rssUrlDict.Add(tbFavoriteName.Text, cbUrl.Text);
                cbUrl.DataSource = rssUrlDict.Keys.ToList();
            }
        }

        private void btfavoriteDelete_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(cbUrl.Text)) {
                return;
            } else if (rssUrlDict.ContainsKey(cbUrl.Text)) {
                rssUrlDict.Remove(cbUrl.Text);
                tbFavoriteName.Text = null;
                cbUrl.DataSource = rssUrlDict.Keys.ToList();
            }
        }

        private void lbTitles_DrawItem_1(object sender, DrawItemEventArgs e) {
            var idx = e.Index;                                                      //描画対象の行
            if (idx == -1) return;                                                  //範囲外なら何もしない
            var sts = e.State;                                                      //セルの状態
            var fnt = e.Font;                                                       //フォント
            var _bnd = e.Bounds;                                                    //描画範囲(オリジナル)
            var bnd = new RectangleF(_bnd.X, _bnd.Y, _bnd.Width, _bnd.Height);     //描画範囲(描画用)
            var txt = (string)lbTitles.Items[idx];                                  //リストボックス内の文字
            var bsh = new SolidBrush(lbTitles.ForeColor);                           //文字色
            var sel = (DrawItemState.Selected == (sts & DrawItemState.Selected));   //選択行か
            var odd = (idx % 2 == 1);                                               //奇数行か
            var fore = Brushes.WhiteSmoke;                                         //偶数行の背景色
            var bak = Brushes.AliceBlue;                                           //奇数行の背景色

            e.DrawBackground();                                                     //背景描画

            //奇数項目の背景色を変える（選択行は除く）
            if (odd && !sel) {
                e.Graphics.FillRectangle(bak, bnd);
            } else if (!odd && !sel) {
                e.Graphics.FillRectangle(fore, bnd);
            }

            //文字を描画
            e.Graphics.DrawString(txt, fnt, bsh, bnd);
        }
    }
    }
}
