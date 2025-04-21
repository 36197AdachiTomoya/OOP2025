namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィートとメートルの対応表を出力する
        static void Main(string[] args) {
            
            //string から intに変換 
            int start = int.Parse(args[1]);
            int end = int.Parse(args[2]);

            if(args.Length >= 1 && args[0] == "-tom") {
                PrintFeetToMeterlist(start, end);
            } else {
                PrintFeetToMeterlist(start, end);
            }

            /*if (args.Length >= 1 && args[0] == "-tom") {
                for (int feet = s; feet <= e; feet++) {
                    //double meter = feet * 0.3048;
                    double meter = FeetToMeter(feet);
                    Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                }
            } else if (args.Length >= 1 && args[0] == "-tof") {
                for (int meter = s; meter <= e; meter++) {
                    //double meter = feet * 0.3048;
                    double feet = MeterToFeet(meter);
                    Console.WriteLine($"{meter}m = {feet:0.0000}ft");
                }
            }*/
        }
        static void PrintFeetToMeterlist(int start ,int stop) {
            for(int meter = start; meter <= stop; meter++) {
                double feet = MeterToFeet(meter);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
            }
        }

        static void PrintMeterToFeetlist() {

        }

        static double FeetToMeter(int feet) {
            return feet * 0.3048;
        }

        static double MeterToFeet(int meter) {
            return meter / 0.3048;
        }
    }
}
