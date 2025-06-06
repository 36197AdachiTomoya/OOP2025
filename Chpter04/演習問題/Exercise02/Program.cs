using System;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            
            Exercise1();
            Console.WriteLine("----");
            Exercise2();
            Console.WriteLine("----");
            Exercise3();
            
        }
        private static void Exercise1() {
            int i = int.Parse(Console.ReadLine());
            if (i < 0) {
                Console.WriteLine(i);
            } else if (i < 100) {
                Console.WriteLine(i * 2);
            } else if (i < 500) {
                Console.WriteLine(i * 3);
            } else {
                Console.WriteLine(i);
            }
        }
        private static void Exercise2() {
            int i = int.Parse(Console.ReadLine());
            switch (i) {
                case < 0:
                    Console.WriteLine(i);
                    break;
                case < 100:
                    Console.WriteLine(i * 2);
                    break;
                case < 500:
                    Console.WriteLine(i * 3);
                    break;
                default:
                    Console.WriteLine(i);
                    break;
            }
        }
        private static void Exercise3() {
            int i = int.Parse(Console.ReadLine());
            var text = i switch {
                < 0 => i,
                < 100 => i * 2,
                < 500 => i * 3,
                _ => i
            };
        }
    }
}
