using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IInvoiceService
    {
        public Task<Invoice> Get(int invoiceId);
        public Task<bool> Create(Invoice invoice, int orderId);


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
            throw new NotFoundException($"Invoice with ID {invoiceId} not found.");
        }

        public async Task<bool> Create(Invoice invoice, int orderId)
        {
            if (invoice != null)
            {
                var invoiceNumber = await CreateInvoiceNumber(invoice);
                invoice.InvoiceNumber = invoiceNumber;
                invoice.DueDate = DateTime.Now.AddDays(14);

                _dbContext.Invoices.Add(invoice);
                _dbContext.SaveChanges();

                var order = _dbContext.Orders.Find(orderId);

                if (order != null)
                {
                    order.InvoiceId = invoice.Id;
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            throw new NotFoundException(nameof(invoice));
        }

        public async Task<string> CreateInvoiceNumber(Invoice invoice)
        {
            string? lastInvoice = null;
            try
            {
                lastInvoice = await _dbContext.Invoices.OrderBy(p => p.Id).Select(p => p.InvoiceNumber).LastOrDefaultAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (lastInvoice == null)
            {
                int firstNumber = 0;
                invoice.InvoiceNumber = firstNumber + invoice.InvoiceDate.ToString("yyyyMMdd");    
                return invoice.InvoiceNumber;
            }
            else
            {
                var lengthInvoiceNumber = lastInvoice.Length;
                string numberInvoiceWithoutDate = lastInvoice.Substring(0, lengthInvoiceNumber - 8);
                int numberWithoutDate = int.Parse(numberInvoiceWithoutDate);
                numberWithoutDate++;
                invoice.InvoiceNumber = numberWithoutDate + invoice.InvoiceDate.ToString("yyyyMMdd");
                return invoice.InvoiceNumber;
            }
        }
    }
}
