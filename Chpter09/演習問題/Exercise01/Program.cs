using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var datetime = DateTime.Now;
            DisplayDatePattern1(datetime);
            DisplayDatePattern2(datetime);
            DisplayDatePattern3(datetime);
        }

        private static void DisplayDatePattern1(DateTime datetime) {
            Console.WriteLine(string.Format("{0:yyyy/MM/dd HH/mm}", datetime));

        }

        private static void DisplayDatePattern2(DateTime datetime) {
            Console.WriteLine(datetime.ToString("yyyy年MM月dd日 HH時mm分ss秒"));
        }

        private static void DisplayDatePattern3(DateTime datetime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = datetime.ToString("ggyy年M月d日", culture);
            var dayOfweek = culture.DateTimeFormat.GetDayName(datetime.DayOfWeek);
            Console.WriteLine(str + $"({dayOfweek})");
                
        }
    }
}
