using System;
using System.IO;
using System.Linq;

namespace CountClass {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "source.txt";
            var lines = File.ReadAllLines(filePath); 

            int cnt = 0;

            foreach (string line in lines) {
                string[] text = line.Split('　'); 
                int c = text.Count(s => s.Trim() == "class"); 
                if (c > 0) {
                    cnt++;
                }
            }

            Console.WriteLine(cnt);
        }
    }
}
