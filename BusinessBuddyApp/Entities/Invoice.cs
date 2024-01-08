namespace BusinessBuddyApp.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        //private int OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        //płatność do kiedy powinna zostać dokonana płatność
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; } = false;

    }
}
