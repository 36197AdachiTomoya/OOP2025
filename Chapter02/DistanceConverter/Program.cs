
namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィートとメートルの対応表を出力する
        static void Main(string[] args) {

            //string から intに変換 
            int start = int.Parse(args[1]);
            int end = int.Parse(args[2]);

            if (args.Length >= 1 && args[0] == "-tom") {
                PrintFeetToMeterlist(start, end);
            } else {
                PrintMeterToFeetlist(start, end);
            }
        }

        static void PrintMeterToFeetlist(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                double feet = FeetConverter.FromMeter(meter);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
            }
        }

        static void PrintFeetToMeterlist(int start, int end) {
            for (int feet = start; feet <= end; feet++) {
                double meter = FeetConverter.FromMeter(feet);
                Console.WriteLine($"{meter}ft = {feet:0.0000}m");
            }

        }
    }
}


