using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? OrderDetailId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.InProgress;
        public string Notes { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double FinalAmount { get; set; }
        public int DeliveryId { get; set; }
        // Order Product 
        public double TotalAmount { get; set; } = 0;
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        //Product 

        public string ProductType { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }

        // Client
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }
        //Invoice
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; } = false;

    }
}
