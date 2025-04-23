using System.Diagnostics.Metrics;
using System.Xml.Serialization;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("はじめ：");
            int start = int.Parse(Console.ReadLine());
            Console.Write("おわり：");
            int end = int.Parse(Console.ReadLine());

            MeterToInch(start, end);
           /* InchToMeter(1, 10);*/
        }
           /* static void InchToMeter(int s, int e) {
                for (int start = s; start >= e; start++) {
                    double meter = InchConverter.ToMeter(start);
                    Console.WriteLine($"{start}inch = {meter:0.0000}m");
                }
            }*/

            static void MeterToInch(int start, int end) {
                for (int s = start; s <= end; s++) {
                double inch = InchConverter.ToInch(s);
                Console.WriteLine($"{s}inch = {inch:0.0000}m");
                }
            }
    }
}
