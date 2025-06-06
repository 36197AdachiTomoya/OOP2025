namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {
            var numbers = Enumerable.Range(1, 10);

            Console.WriteLine(numbers.Where(n => n % 2 == 0).Max());
            
        }
    }
}
