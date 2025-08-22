using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            string[] texts = [
                "Time is money.",
                "What time is it?",
                "It will take time.",
                "We reorganized the timetable.",
            ];

            foreach(var line in texts) {
                var regex = new Regex(@"\btime\b",RegexOptions.IgnoreCase);
                var match = regex.Match(line);
                if (match.Success) {
                    Console.WriteLine($"{line},{match.Index}");
                }
            }

        }
    }
}
