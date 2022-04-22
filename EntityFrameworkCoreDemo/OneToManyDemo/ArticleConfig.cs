using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneToManyDemo
{
    class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.Property(a => a.Title).IsRequired().IsUnicode().HasMaxLength(50);
            builder.Property(a => a.Content).IsRequired().IsUnicode();
        }
    }
}
