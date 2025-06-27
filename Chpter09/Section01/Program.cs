using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            //var today = new DateTime(2025,7,12); //日付
            //var now = DateTime.Now;     //日付と時刻

            //Console.WriteLine($"Today:{today.Month}");
            //Console.WriteLine($"Now:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            Console.Write("西暦：");
            var year = int.Parse(Console.ReadLine());
            Console.Write("月：");
            var month = int.Parse(Console.ReadLine());
            Console.Write("日：");
            var day = int.Parse(Console.ReadLine());

            var birth = new DateTime(year, month, day);


            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            
            var str = birth.ToString("ggyy年M月d日", culture);
            var shortDayOfWeek = culture.DateTimeFormat.GetShortestDayName(birth.DayOfWeek);

            //Console.WriteLine(str + shortDayOfWeek + "曜日");
            Console.WriteLine(str + birth.ToString("ddd曜日", culture));

            //③生まれてから〇〇〇〇日目です

            //TimeSpan diff;
            //while (true) {
            //    diff = DateTime.Now - birth;
            //    Console.Write($"\r{diff.TotalDays}日 {diff.TotalHours}時間 {diff.TotalMinutes}分"); //生まれてからの経過秒数
            //}



            //④あなたは〇〇歳です！

            int age = GetAge(birth, DateTime.Today);

            Console.WriteLine(age + "歳");

            //⑤1月1日から何日目か？
            var today = DateTime.Today;
            int dayOfYear = today.DayOfYear;
            Console.WriteLine(dayOfYear);



            //②うるう年の判定プログラムを作成する
            //西暦を入力
            //  →〇〇〇〇年はうるう年です
            //  →〇〇〇〇年は平年です


        }
        static int GetAge(DateTime birthday, DateTime targetDay) {
            var age = targetDay.Year - birthday.Year;
            if (targetDay < birthday.AddYears(age)) {
                age--;
            }
            return age;
        }
    }
}
