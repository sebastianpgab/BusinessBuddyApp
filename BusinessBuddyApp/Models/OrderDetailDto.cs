using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Models
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.InProgress;
        public string Notes { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double FinalAmount { get; set; }
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
    }
}
