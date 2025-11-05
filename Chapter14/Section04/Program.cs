using System.Threading.Tasks;

namespace Section04 {
    internal class Program {
        static async Task Main(string[] args) {
            HttpClient client = new HttpClient();
            await GethtmlExample(client);
        }

        static async Task GethtmlExample(HttpClient httpClient) {
            var url = "https://www.yahoo.co.jp";
            var text = await httpClient.GetStringAsync(url);
            Console.WriteLine(text);
        }
    }
}
