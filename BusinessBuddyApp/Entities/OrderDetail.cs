namespace BusinessBuddyApp.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
