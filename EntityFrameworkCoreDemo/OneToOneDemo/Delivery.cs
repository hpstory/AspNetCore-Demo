namespace OneToOneDemo
{
    public class Delivery
    {
        public long Id { get; set; }

        public string CompanyName { get; set; }

        public string Number { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }
    }
}
