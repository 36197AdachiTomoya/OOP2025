using System.Diagnostics.Metrics;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            InchToMeter(1, 10);
        }
        static void InchToMeter(int s, int end) {
            for (int start = s; start >= end; start++) {
                double meter = InchConverter.ToMeter(start);
                Console.WriteLine($"{start}inch = {meter:0.0000}m");
            }
        }
    }
}
