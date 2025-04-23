using System.Diagnostics.Metrics;
using System.Threading;
using System.Xml.Serialization;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("はじめ：");
            int start = int.Parse(Console.ReadLine());
            Console.Write("おわり：");
            int end = int.Parse(Console.ReadLine());

            YardToMeter(start, end);
            MeterToYard(start, end);

            //MeterToInch(start, end);
            //InchToMeter(1, 10);
        }

        //インチからメートルに変換
        /* static void InchToMeter(int s, int e) {
             for (int start = s; start >= e; start++) {
                 double meter = InchConverter.ToMeter(start);
                 Console.WriteLine($"{start}inch = {meter:0.0000}m");
             }
         }*/

        //メートルからインチに変換
        /* static void MeterToInch(int start, int end) {
             for (int s = start; s <= end; s++) {
                 double inch = InchConverter.ToInch(s);
                 Console.WriteLine($"{s}inch = {inch:0.0000}m");
             }
         }*/

        //ヤードからメートルに変換
        static void YardToMeter(int start, int end) {
            for (int s = start; s <= end; s++) {
                double meter = YardConverter.ToMeter(s);
                Console.WriteLine($"{s}yard = {meter:0.0000}m");
            }
        }
        //メートルからヤードに変換
        static void MeterToYard(int start, int end) {
            for (int s = start; s <= end; s++) {
                double yard = YardConverter.ToYard(s);
                Console.WriteLine($"{s}meter = {yard:0.0000}yard");
            }
        }
    }
}


