namespace BusinessBuddyApp.Models
{
    public class OrderProductDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; } = 0;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
    }
}
