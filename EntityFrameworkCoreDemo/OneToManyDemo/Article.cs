namespace OneToManyDemo
{
    public class Article
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<Comment> Comments { get; } = new List<Comment>();
    }
}
