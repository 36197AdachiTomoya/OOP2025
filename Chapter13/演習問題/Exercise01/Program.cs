
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();

            Exercise1_3();
            Console.WriteLine();

            Exercise1_4();
            Console.WriteLine();

            Exercise1_5();
            Console.WriteLine();

            Exercise1_6();
            Console.WriteLine();

            Exercise1_7();
            Console.WriteLine();

            Exercise1_8();
            Console.WriteLine();
        }

        private static void Exercise1_2() {
            var book = Library.Books.MaxBy(b => b.Price);
            Console.WriteLine(book);
        }

        private static void Exercise1_3() {
            var results = Library.Books
                          .GroupBy(b => b.PublishedYear)
                          .OrderBy(b => b.Key)
                          .Select(b => new {
                              PublishedYear = b.Key,
                              count = b.Count()
                          });
            foreach (var item in results) {
                Console.WriteLine($"{item.PublishedYear}:{item.count}");
            }
        }

        private static void Exercise1_4() {
            var books = Library.Books
                               .OrderByDescending(b => b.PublishedYear)
                               .ThenByDescending(b => b.Price);
            foreach (var book in books) {
                Console.WriteLine(book);
            }
        }

        private static void Exercise1_5() {
            var books = Library.Books
                            .Where(book => book.PublishedYear == 2022)
                            .Join(Library.Categories
                                     , book => book.CategoryId
                                     , category => category.Id
                                     , (book, category) => category.Name
                                 ).Distinct();



            foreach (var book in books) {
                Console.WriteLine(book);
            }
        }

        private static void Exercise1_6() {
            
        }

        private static void Exercise1_7() {
            
        }

        private static void Exercise1_8() {
            
        }
    }
}
