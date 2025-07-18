using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private void btRssGet_Click(object sender, EventArgs e) {
            using(var wc = new WebClient()) {
                var url = wc.OpenRead(tbUrl.Text);
                XDocument xdoc = XDocument.Load(url);  //RSSの取得

                //RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item").Select
                    (x => new ItemData { Title = (string)x.Element("title"),}).ToList();

                //リストボックスへタイトル表示
                foreach(var s in items) {
                    lbTitles.Items.Add(s.Title);
                }

            }
        }
    }
}
