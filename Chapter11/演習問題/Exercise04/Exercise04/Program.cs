using System.Text.RegularExpressions;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var lines = File.ReadAllText("sample.txt");
            //問題１１．４
            
            var newlines = Regex.Replace(lines,@"(V|v)ersion\s*=\s*""v4\.0""","version=v5.0");

            File.WriteAllText("sampleChange.txt",newlines);

            //これ以降は確認用
            var text = File.ReadAllText("sampleChange.txt");
            Console.WriteLine(text);
        }
    }
}
