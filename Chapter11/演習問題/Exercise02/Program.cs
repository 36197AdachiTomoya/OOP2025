using System.Text.RegularExpressions;
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
            Console.WriteLine("=========================");
            Pickup3Word(filePath);
        }

        private static void Pickup3DigitNumber(string filePath) {
            foreach (var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line,@"\b\d{3,}\b");
                foreach(Match m in matches) {
                    //結果を出力
                    Console.WriteLine(m);
                }
            }
        }

        private static void Pickup3Word(string filePath) {
            foreach (var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line, @"\b[a-zA-Z]{3,}\b");
                foreach (Match m in matches) {
                    //結果を出力
                    Console.WriteLine(m);
                }
            }
        }
    }
}
