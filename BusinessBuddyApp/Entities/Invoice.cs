namespace BusinessBuddyApp.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public virtual Client Client { get; set; }
        public bool IsPaid { get; set; }

    }
}
