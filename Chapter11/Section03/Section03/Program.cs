using System.Text.RegularExpressions;
namespace Section03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "oijefwoiワヲン";

            Match match = Regex.Match(text, @"\p{IsKatakana}+");
            if (match.Success) {
                Console.WriteLine($"{match.Index}, {match.Value}");
            }
            var matches = Regex.Matches(text, @"\p{IsKatakana}+");
            foreach(Match match2 in matches) {
                Console.WriteLine($"Index={match.Index},Length={match.Length},Value{match.Value}");
            }
        }
    }
}
