namespace BusinessBuddyApp.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? OrderDetailId { get; set; }
        public int? InvoiceId { get; set; }
    }
}
