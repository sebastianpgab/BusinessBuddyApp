namespace BusinessBuddyApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? OrderDetailId { get; set; }
        public int? InvoiceId { get; set; }
        public virtual Client? Client {get; set;}
        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual Invoice? Invoice { get; set; }

    }
}
