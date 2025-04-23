using System.Diagnostics.Metrics;
using System.Threading;
using System.Xml.Serialization;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("１：ヤードからメートル");
            Console.WriteLine("２：メートルからヤード");
            int i = int.Parse(Console.ReadLine());
            if (i == 1) {
                Console.Write("変換前：");
                int j = int.Parse(Console.ReadLine());
                YardToMeter(j);
            } else {
                Console.Write("変換前：");
                int j = int.Parse(Console.ReadLine());
                MeterToYard(j);
            }
            //YardToMeter(start, end);
            //MeterToYard(start, end);

            //MeterToInch(start, end);
            //InchToMeter(1, 10);
        }

        //インチからメートルへの対応表を出力
       /* static void InchToMeter(int s, int e) {
            for (int start = s; start >= e; start++) {
                double meter = InchConverter.ToMeter(start);
                Console.WriteLine($"{start}inch = {meter:0.0000}m");
            }
        }*/

        //メートルからインチへの対応表を出力
        /* static void MeterToInch(int start, int end) {
             for (int s = start; s <= end; s++) {
                 double inch = InchConverter.ToInch(s);
                 Console.WriteLine($"{s}inch = {inch:0.0000}m");
             }
         }*/

        //ヤードからメートルへ変換
        static void YardToMeter(int i) {
            double meter = YardConverter.ToMeter(i);
            Console.WriteLine($"{i}yard = {meter:0.0000}m");
        }
        //メートルからヤードへ変換
        static void MeterToYard(int i) {
            double yard = YardConverter.ToYard(i);
            Console.WriteLine($"{i}meter = {yard:0.0000}yard");
        }
    }
}


