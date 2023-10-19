namespace BusinessBuddyApp.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public virtual Order Order { get; set; }
        public bool IsPaid { get; set; }

    }
}
