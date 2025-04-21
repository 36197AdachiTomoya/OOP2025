namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {
            Product karinto = new Product(123, "かりんとう", 180);

            Product daihuku = new Product(124, "大福", 200);
            //大福    
            //税抜きの価格を表示

            Console.WriteLine(daihuku.Name + "の税抜き価格は" + daihuku.Price + "です");

            //消費税額の表示

            Console.WriteLine(daihuku.Name + "の消費税価格は" + daihuku.GetTax() + "です");

            //税込み価格の表示

            Console.WriteLine(daihuku.Name + "の税込み価格は" + daihuku.GetPriceincledingTax() + "です");


            //かりんとう
            //税抜きの価格を表示

            Console.WriteLine(karinto.Name + "の税抜き価格は" + karinto.Price + "です");

            //消費税額の表示

            Console.WriteLine(karinto.Name + "の消費税価格は" + karinto.GetTax() + "です");

            //税込み価格の表示

            Console.WriteLine(karinto.Name + "の税込み価格は" + karinto.GetPriceincledingTax() + "です");
        }
    }
}
