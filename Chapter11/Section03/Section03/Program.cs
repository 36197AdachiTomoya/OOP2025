﻿using System.Text.RegularExpressions;
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

            var text2 = "private List<string> results = new List<string>();";
            var matches2 = Regex
                .Matches(text2, @"\b[a-z]+\b")
                .Cast<Match>()
                .OrderBy(x => x.Length);
            foreach(Match match3 in matches2) {
                Console.WriteLine
                    ($"Index={match3.Index},Length={match3.Length},Value={match3.Value}");
            }







        }
    }
}
