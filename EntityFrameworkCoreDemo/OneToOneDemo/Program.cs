using Microsoft.EntityFrameworkCore;
using OneToOneDemo;

using (DemoDbContext dbContext = new())
{
    var order = new Order
    {
        Address = "999号",
        Name = "USB充电器"
    };

    var delivery = new Delivery
    {
        CompanyName = "快递",
        Number = "123456",
        Order = order
    };

    dbContext.Deliveries.Add(delivery);
    await dbContext.SaveChangesAsync();

    Order getOrder = await dbContext.Orders.Include(o => o.Delivery)
        .FirstAsync(o => o.Name.Contains("充电器"));
    Console.WriteLine(getOrder.Name);
}
