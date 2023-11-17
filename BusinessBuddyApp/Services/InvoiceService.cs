using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IInvoiceService
    {
        public Task<Invoice> Get(int invoiceId);
        public bool Create(Invoice invoice, int orderId);

    }
    public class InvoiceService : IInvoiceService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public InvoiceService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Invoice> Get(int invoiceId)
        {
            var invoice = await _dbContext.Invoices.FindAsync(invoiceId);
            if(invoice != null)
            {
                return invoice;

            }
            throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");
        }

        public bool Create(Invoice invoice, int orderId)
        {
            if (invoice != null)
            {
                var invoiceNumber = CreateInvoiceNumber(invoice).ToString();
                invoice.InvoiceNumber = invoiceNumber;
                invoice.DueDate = DateTime.Now.AddDays(14);
                _dbContext.Invoices.Add(invoice);
                _dbContext.SaveChanges();
                var order = _dbContext.Orders.Find(orderId);
                if (order != null)
                {
                    order.InvoiceId = invoice.Id;
                    _dbContext.SaveChanges();

                }
                return true;
            }
            throw new ArgumentNullException(nameof(invoice));
        }

        public async Task<string> CreateInvoiceNumber(Invoice invoice)
        {
            string? lastInvoice = await _dbContext.Invoices.Select(p => p.InvoiceNumber).LastOrDefaultAsync();

            if (lastInvoice != null)
            {
                int firstNumber = 0;
                invoice.InvoiceNumber = firstNumber + invoice.InvoiceDate.ToString("yyyy-mm-dd");    
                return invoice.InvoiceNumber;
            }
            else
            {
                var lengthInvoiceNumber = invoice.InvoiceNumber.Length;
                string numberInvoiceWithoutDate = invoice.InvoiceNumber.Substring(lengthInvoiceNumber - 8);
                int numberWithoutDate = int.Parse(numberInvoiceWithoutDate);
                numberWithoutDate++;
                invoice.InvoiceNumber = numberWithoutDate + invoice.InvoiceDate.ToString("yyyy-mm-dd");
                return invoice.InvoiceNumber;
            }
        }
    }
}
