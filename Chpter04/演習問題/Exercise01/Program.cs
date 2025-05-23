
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
    "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
    "JavaScript", "Swift", "Go",
];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise3(List<string> langs) {
            //foreach
            foreach (var s in langs) {
                if (s.Contains("S")) {
                    Console.WriteLine(s);
                }
            }
            //for
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains("S")) {
                    Console.WriteLine(langs[i]);
                }
            }
            //while
            int j = 0;
            while (j < langs.Count) {
                if (langs[j].Contains("S")) {
                    Console.WriteLine(langs[j]);
                }
                j++;
            }
        }

        private static void Exercise2(List<string> langs) {
            langs.Where(s => s.Contains("S")).ToList().ForEach(s => Console.WriteLine(s));
            
        }

        private static void Exercise1(List<string> langs) {
            var find = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(find);
        }
    }
}
