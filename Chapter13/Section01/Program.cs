namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var books = Library.Categories
                            .GroupJoin(Library.Books
                                     , c => c.Id
                                     , b => b.CategoryId
                                     , (c, books) => new {
                                         Category = c.Name,
                                         Books = books,
                                     });
                                       
            
        }
    }
}
