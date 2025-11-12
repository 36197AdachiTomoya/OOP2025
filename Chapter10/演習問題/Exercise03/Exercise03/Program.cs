namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var filePath = Console.ReadLine();
            var filePath2 = Console.ReadLine();

            var lines = File.ReadAllLines(filePath2);

            using (var writer = new StreamWriter(filePath, append: true)) {
                foreach (var line in lines) {
                    writer.WriteLine(line);
                }
            }

        }
    }
}