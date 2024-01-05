using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Models
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
