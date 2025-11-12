namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<IGreatingBase> list = [
                new GreatingMorning(),
                new GreatingAfternoon(),
                new GreatingEvening(),
                ];

            foreach (var obj in list) {
                string msg = obj.GetMessage();
                Console.WriteLine(msg);
            }
        }
    }

    class GreatingMorning : IGreatingBase{
        public string GetMessage() => "おはよう";
    }

    class GreatingAfternoon : IGreatingBase {
        public string GetMessage() => "こんにちは";
    }

    class GreatingEvening : IGreatingBase {
        public string GetMessage() => "こんばんは";
    }

}
