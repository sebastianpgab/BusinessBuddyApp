namespace BusinessBuddyApp.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; } = 0;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual Product? Product { get; set; }
    }
}
