using EntityFrameworkCoreDemo;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

using (DemoDbContext dbContext = new())
{
    // create
    //var book1 = new Book
    //{
    //    Title = "零基础趣学C语言",
    //    Price = 59.8M,
    //    PublishTime = new DateTime(2019, 3, 1)
    //};
    //var book2 = new Book
    //{
    //    Title = "算法(第4版)",
    //    Price = 99,
    //    PublishTime = new DateTime(2012, 10, 1)
    //};
    //var book3 = new Book
    //{
    //    Title = "数学之美",
    //    Price = 69,
    //    PublishTime = new DateTime(2020, 5, 1)
    //};
    //var book4 = new Book
    //{
    //    Title = "程序员的SQL金典",
    //    Price = 52,
    //    PublishTime = new DateTime(2008, 9, 1)
    //};
    //var book5 = new Book
    //{
    //    Title = "文明之光",
    //    Price = 246,
    //    PublishTime = new DateTime(2017, 3, 1)
    //};
    //dbContext.Books.Add(book1);
    //dbContext.Books.Add(book2);
    //dbContext.Books.Add(book3);
    //dbContext.Books.Add(book4);
    //dbContext.Books.Add(book5);
    //await dbContext.SaveChangesAsync();

    // read
    //var books = dbContext.Books.Where(b => b.Price >= 80);
    //foreach (var book in books)
    //{
    //    Console.WriteLine(book.Price);
    //}

    // update
    //var updatedBook = dbContext.Books.First();
    //updatedBook.Title = "C#";
    //await dbContext.SaveChangesAsync();

    // delete
    //var deletedBook = dbContext.Books.Single(b => b.Title == "文明之光");
    //dbContext.Books.Remove(deletedBook);
    //await dbContext.SaveChangesAsync();
}
