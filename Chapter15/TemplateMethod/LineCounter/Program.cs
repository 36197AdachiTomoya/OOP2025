using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            string filename = string.Empty;
            Console.WriteLine("ファイルの入力：");
            filename = Console.ReadLine() ?? string.Empty;

            if (!File.Exists(filename)) {
                Console.WriteLine("存在なし");
                return;
            }
            TextProcessor.Run<LineCounterProcessor>(filename);
        }    
        
    }
}
