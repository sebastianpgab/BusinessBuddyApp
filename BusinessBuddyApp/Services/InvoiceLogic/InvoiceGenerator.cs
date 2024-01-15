using BusinessBuddyApp.Entities;
using System.IO;
using System.Text;
using SelectPdf;
using Microsoft.EntityFrameworkCore;
using BusinessBuddyApp.Exceptions;
using BusinessBuddyApp.Models;

public interface IInvoiceGenerator
{
    Task GenerateInvoice(Invoice invoice, string directoryPath);
    Task<string> GetHTMLString(Invoice invoice);
}

public class InvoiceGenerator : IInvoiceGenerator
{
    private readonly BusinessBudyDbContext _dbContext;
    public InvoiceGenerator(BusinessBudyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> GetHTMLString(Invoice invoice)
    {
        var order =  _dbContext.Orders.FirstOrDefault(p => p.InvoiceId == invoice.Id);

        if(order != null)
        {
            var clientOrder = await GetOrderWithClient(order.Id);
            string htmlContent = File.ReadAllText("wwwroot/templates/faktura.html");

            // Client data
            htmlContent = htmlContent.Replace("[FirstNameBuyer]", clientOrder.Client.FirstName);
            htmlContent = htmlContent.Replace("[LastNameBuyer]", clientOrder.Client.LastName);
            htmlContent = htmlContent.Replace("[PhoneNumberBuyer]", clientOrder.Client.PhoneNumber);
            htmlContent = htmlContent.Replace("[EmailBuyer]", clientOrder.Client.Email);
            // Address
            htmlContent = htmlContent.Replace("[CityBuyer]", clientOrder.Client.Address.City);
            htmlContent = htmlContent.Replace("[ApartmentNumberBuyer]", clientOrder.Client.Address.ApartmentNumber);
            htmlContent = htmlContent.Replace("[BuildingNumberBuyer]", clientOrder.Client.Address.BuildingNumber);

            htmlContent = htmlContent.Replace("[PostalCodeBuyer]", clientOrder.Client.Address.PostalCode);
            htmlContent = htmlContent.Replace("[StreetBuyer]", clientOrder.Client.Address.Street);
            // Invoice
            htmlContent = htmlContent.Replace("[InvoiceNumber]", clientOrder.Invoice.InvoiceNumber);
            htmlContent = htmlContent.Replace("[DueDate]", (clientOrder.Invoice.DueDate).ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("[IsInvoicePaid]", clientOrder.Invoice.IsPaid.ToString());
            htmlContent = htmlContent.Replace("[InvoiceDate]", clientOrder.Invoice.InvoiceDate.ToString("yyyy-MM-dd"));
            // Order details
            htmlContent = htmlContent.Replace("[OrderStatus]", clientOrder.OrderDetail.Status.ToString());
            htmlContent = htmlContent.Replace("[FinalAmount]", clientOrder.OrderDetail.FinalAmount.ToString());
            htmlContent = htmlContent.Replace("[OrderNotes]", clientOrder.OrderDetail.Notes);
            htmlContent = htmlContent.Replace("[PaymentMethod]", clientOrder.OrderDetail.PaymentMethod.ToString());
            htmlContent = htmlContent.Replace("[OrderCompletionDate]", clientOrder.OrderDetail.CompletionDate?.ToString("yyyy-MM-dd") ?? "no date");
            htmlContent = htmlContent.Replace("[OrderDate]", clientOrder.OrderDetail.OrderDate.ToString("yyyy-MM-dd"));
            // Ordered products
            int totalQuantity = clientOrder.OrderDetail.OrderProducts.Sum(p => p.Quantity);
            htmlContent = htmlContent.Replace("[TotalProductQuantity]", totalQuantity.ToString());
            var totalAmount = clientOrder.OrderDetail.OrderProducts.Sum(p => p.TotalAmount);
            htmlContent = htmlContent.Replace("[CostPerItemType]", totalAmount.ToString("C")); // Formatting as currency value
            var products = clientOrder.OrderDetail.OrderProducts.ToList();

            var productRows = new StringBuilder();
            foreach (var product in products)
            {
                productRows.AppendLine($"<tr><td>{product.Product.ProductType}</td><td>{product.Quantity}</td><td>{product.Product.Price} zł</td><td>{product.TotalAmount} zł</td></tr>");
            }
            htmlContent = htmlContent.Replace("<!--Products-->", productRows.ToString());

            return htmlContent;
        }    

        throw new NotFoundException($"Invoice with was not found.");
    }

    public async Task GenerateInvoice(Invoice invoice, string directoryPath)
    {
        string htmlString = await GetHTMLString(invoice);

        HtmlToPdf converter = new HtmlToPdf();
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        PdfDocument doc = converter.ConvertHtmlString(htmlString);

        string filePath = Path.Combine(directoryPath, $"{invoice.InvoiceNumber}.pdf");
        doc.Save(filePath);
        doc.Close();
    }

    public async Task<Order> GetOrderWithClient(int orderId)
    {
        var order =  await _dbContext.Orders
               .Include(o => o.OrderDetail)
                   .ThenInclude(od => od.OrderProducts)
                       .ThenInclude(op => op.Product)
               .Include(o => o.OrderDetail)
                   .ThenInclude(od => od.DeliveryAddress)
               .Include(o => o.Client)
               .Include(o => o.Invoice)
               .FirstOrDefaultAsync(p => p.Id == orderId);

        if (order == null)
        {
            throw new NotFoundException($"Order not found");
        }

        return order;
    }

}
