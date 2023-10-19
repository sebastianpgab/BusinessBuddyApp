namespace BusinessBuddyApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public int OrderDatailId { get; set; }
        public int InvoiceId { get; set; }
        public virtual Client Client {get; set;}
        public virtual List<OrderDetail> OrderDetails { get; set; } 
        public virtual Invoice Invoice { get; set; }

    }
}
