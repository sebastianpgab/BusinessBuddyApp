namespace BusinessBuddyApp.Entities
{
    public enum OrderStatus
    {
        InProgress,
        Completed,
        Cancelled,
    }

    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        BankTransfer,
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.InProgress;
        public string Notes { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double FinalAmount { get; set; }
        public int DeliveryId { get; set; }
        public Address? DeliveryAddress { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public int? IdCompany { get; set; }
    }
}
