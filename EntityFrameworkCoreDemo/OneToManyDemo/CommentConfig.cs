using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneToManyDemo
{
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasOne(c => c.Article).WithMany(a => a.Comments).IsRequired();
            builder.Property(c => c.Message).IsRequired();
        }
    }
}
