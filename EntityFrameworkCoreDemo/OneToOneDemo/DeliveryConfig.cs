using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneToOneDemo
{
    class DeliveryConfig : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries");
            builder.Property(d => d.CompanyName).IsUnicode().HasMaxLength(10);
            builder.Property(d => d.Number).HasMaxLength(50);
        }
    }
}
