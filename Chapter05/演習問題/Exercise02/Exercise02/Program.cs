using Exercise01;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var ymCollection = new YearMonth[] {
                new YearMonth(1980, 1),
                new YearMonth(1990, 4),
                new YearMonth(2000, 7),
                new YearMonth(2010, 9),
                new YearMonth(2024, 12),
            };

            Console.WriteLine("5.2.2");
            Exercise2(ymCollection);

            
            

            Console.WriteLine("5.2.4");
            Exercise4(ymCollection);


            Console.WriteLine("5.2.5");
            Exercise5(ymCollection);
        }

        private static YearMonth FindFirst21C(YearMonth[] ymCollection) {
            foreach (var s in ymCollection) {
                if (s.Is21stCentury) {
                    return s;
                }
            }
            return null;
        }

        private static void Exercise2(YearMonth[] ymCollection) {
            foreach (var s in ymCollection) {
                Console.WriteLine(s);
            }
        }

        private static void Exercise4(YearMonth[] ymCollection) {
            var a = FindFirst21C(ymCollection);
            if(a is not null) {
                Console.WriteLine(a.Year);
            } else {
                Console.WriteLine("21世紀のデータはありません");
            }
            
        }

        private static void Exercise5(YearMonth[] ymCollection) {
            var s = ymCollection.Select(s => s.AddOneMonth()).ToArray();
            foreach(var item in s) {
                Console.WriteLine(item);
            }
        }
    }
}
