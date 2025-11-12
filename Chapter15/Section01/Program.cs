namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<GreatingBase> list = [
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

    class GreatingMorning : GreatingBase{
        public override string GetMessage() => "おはよう";
    }

    class GreatingAfternoon : GreatingBase {
        public override string GetMessage() => "こんにちは";
    }

    class GreatingEvening : GreatingBase {
        public override string GetMessage() => "こんばんは";
    }

}
