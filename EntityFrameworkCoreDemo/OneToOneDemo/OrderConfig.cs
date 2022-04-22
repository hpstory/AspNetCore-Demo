using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneToOneDemo
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o => o.Address);
            builder.Property(o => o.Name);
            builder.HasOne(o => o.Delivery).WithOne(d => d.Order)
                .HasForeignKey<Delivery>(d => d.OrderId);
        }
    }
}
