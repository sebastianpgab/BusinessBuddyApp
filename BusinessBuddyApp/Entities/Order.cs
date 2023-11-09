namespace BusinessBuddyApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        //to powinno zostać usunięte, bo jest niespójność
        public int? OrderDatailId { get; set; }
        public int? InvoiceId { get; set; }
        public virtual Client Client {get; set;}
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
        public virtual Invoice Invoice { get; set; }

    }
}
