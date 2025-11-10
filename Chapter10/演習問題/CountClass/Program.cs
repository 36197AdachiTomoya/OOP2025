namespace CountClass {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "source.txt";
            using (StreamReader reader = new StreamReader(filePath)) {
                string? line;
                int cnt = 0;
                while((line = reader.ReadLine()) != null) {
                    string[] text = line.Split(' ');
                    int c = text.ToList().Where(s => s.Trim() == "class").Count();
                    if(c > 0) {
                        cnt++;
                    }
                }
                Console.WriteLine(cnt);
            }
        }
    }
}
