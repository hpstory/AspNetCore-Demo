namespace OneToOneDemo
{
    public class Order
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Delivery? Delivery { get; set; }
    }
}
