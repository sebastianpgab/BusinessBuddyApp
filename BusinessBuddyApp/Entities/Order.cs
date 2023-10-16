namespace BusinessBuddyApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public int OrderDatailId { get; set; }
        public virtual Client Client {get; set;}
        public virtual OrderDetail OrderDetail { get; set;}

    }
}
