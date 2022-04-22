using Microsoft.EntityFrameworkCore;
using OneToManyDemo;

using (DemoDbContext dbContext = new DemoDbContext())
{
    // create
    //Article article = new Article();
    //article.Title = "Entity Framework Core 使用HiLo生成主键";
    //article.Content = "HiLo是在NHibernate中生成主键的一种方式，不过现在我们可以在Entity Framework Core中使用。";
    //Comment comment1 = new Comment() { Message = "支持" };
    //Comment comment2 = new Comment() { Message = "微软太牛了" };
    //Comment comment3 = new Comment() { Message = "火钳刘明" };
    //article.Comments.Add(comment1);
    //article.Comments.Add(comment2);
    //article.Comments.Add(comment3);
    //dbContext.Articles.Add(article);
    //await dbContext.SaveChangesAsync();

    // read
    //Article article = dbContext.Articles.Include(
    //    article => article.Comments).Single(a => a.Id == 1);
    //foreach (var comment in article.Comments)
    //{
    //    Console.WriteLine(comment.Message);
    //}

    //Comment comment = dbContext.Comments.Include(
    //    c => c.Article).Single(c => c.Id == 1);
    //Console.WriteLine(comment.Article.Title);


}

