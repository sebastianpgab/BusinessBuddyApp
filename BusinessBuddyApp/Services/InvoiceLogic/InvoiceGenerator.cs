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


            //dane klineta
            htmlContent = htmlContent.Replace("[ImieNabywcy]", clientOrder.Client.FirstName);
             htmlContent = htmlContent.Replace("[NazwiskoNabywcy]", clientOrder.Client.LastName);
             htmlContent = htmlContent.Replace("[NumerTelNabywcy]", clientOrder.Client.PhoneNumber);
             htmlContent = htmlContent.Replace("[EmailNabywcy]", clientOrder.Client.Email);
            //adres
            htmlContent = htmlContent.Replace("[MiastoNabywcy]", clientOrder.Client.Address.City);
            htmlContent = htmlContent.Replace("NumerMieszkaniaNabywcy]", clientOrder.Client.Address.ApartmentNumber);
            htmlContent = htmlContent.Replace("[NumerDomuNabywcy]", clientOrder.Client.Address.BuildingNumber);
            htmlContent = htmlContent.Replace("[KodPocztowyNabywcy]", clientOrder.Client.Address.PostalCode);
            htmlContent = htmlContent.Replace("[UlicaNabywcy]", clientOrder.Client.Address.Street);
            //faktura
            htmlContent = htmlContent.Replace("[NumerFaktury]", clientOrder.Invoice.InvoiceNumber);
            htmlContent = htmlContent.Replace("[MaxPlatnoscData]", (clientOrder.Invoice.DueDate).ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("[CzyOplaconaFaktura]", clientOrder.Invoice.IsPaid.ToString());
            htmlContent = htmlContent.Replace("[DataWystawieniaFaktury]", clientOrder.Invoice.InvoiceDate.ToString("yyyy-MM-dd"));
            //szczegoly zamowienia
            htmlContent = htmlContent.Replace("[StatusZamowienia]", clientOrder.OrderDetail.Status.ToString());
            htmlContent = htmlContent.Replace("[FinalnaKwota]", clientOrder.OrderDetail.FinalAmount.ToString());
            htmlContent = htmlContent.Replace("[NotatkiDoZamowienia]", clientOrder.OrderDetail.Notes);
            htmlContent = htmlContent.Replace("[MetodaPlatnosci]", clientOrder.OrderDetail.PaymentMethod.ToString());
            htmlContent = htmlContent.Replace("[ZakonczenieZamowienia]", clientOrder.OrderDetail.CompletionDate?.ToString("yyyy-MM-dd") ?? "brak daty");
            htmlContent = htmlContent.Replace("[DataZlozeniaZamowienia]", clientOrder.OrderDetail.OrderDate.ToString("yyyy-MM-dd"));
            //zamowione produkty
            int totalQuantity = clientOrder.OrderDetail.OrderProducts.Sum(p => p.Quantity);
            htmlContent = htmlContent.Replace("[IloscZamowieniaProduktu]", totalQuantity.ToString());

            var totalAmount = clientOrder.OrderDetail.OrderProducts.Sum(p => p.TotalAmount);
            htmlContent = htmlContent.Replace("[KosztDanegoTypuTowaru]", totalAmount.ToString("C")); // Formatowanie jako wartość walutowa

            //produkty
            var productTypes = clientOrder.OrderDetail.OrderProducts
                    .Select(p => p.Product.ProductType)
                    .ToList();

            string productTypesString = String.Join(", ", productTypes);
            htmlContent = htmlContent.Replace("[TypProduktu]", productTypesString);






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
