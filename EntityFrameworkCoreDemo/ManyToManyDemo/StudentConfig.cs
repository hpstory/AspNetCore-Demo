using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManyToManyDemo
{
    class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.Property(s => s.Name).IsUnicode().HasMaxLength(20);
            builder.HasMany(s => s.Teachers).WithMany(t => t.Students)
                .UsingEntity(j => j.ToTable("Students_Teachers"));
        }
    }
}
