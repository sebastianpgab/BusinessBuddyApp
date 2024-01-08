using BusinessBuddyApp.Entities;
using System.IO;
using System.Text;
using SelectPdf;
using Microsoft.EntityFrameworkCore;
using BusinessBuddyApp.Exceptions;
using AutoMapper;
using BusinessBuddyApp.Models; // Dodaj tę przestrzeń nazw

public interface IInvoiceGenerator
{
    void GenerateInvoice(Invoice invoice, string directoryPath);
    string GetHTMLString(Invoice invoice);
}

public class InvoiceGenerator : IInvoiceGenerator
{
    private readonly BusinessBudyDbContext _dbContext;
    private readonly IMapper _mapper;
    public InvoiceGenerator(BusinessBudyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = _mapper; 
    }
    public string GetHTMLString(Invoice invoice)
    {
        var order =  _dbContext.Orders.FirstOrDefault(p => p.InvoiceId == invoice.Id);

        if(order != null)
        {
            var orderDto = GetOrderWithClient(order.Id);

            string htmlContent = File.ReadAllText("wwwroot/templates/faktura.html");

             htmlContent = htmlContent.Replace("[ImieNabywcy]", orderDto.FirstName);
             htmlContent = htmlContent.Replace("[NazwiskoNabywcy]", orderDto.LastName);
             htmlContent = htmlContent.Replace("[NumerTelNabywcy]", orderDto.PhoneNumber);
             htmlContent = htmlContent.Replace("[EmailNabywcy]", orderDto.Email);
             htmlContent = htmlContent.Replace("[ZamowienieID]", orderDto.Id.ToString());

            return htmlContent;
        }    

        throw new NotFoundException($"Invoice with was not found.");
    }

    public void GenerateInvoice(Invoice invoice, string directoryPath)
    {
        string htmlString = GetHTMLString(invoice);

        HtmlToPdf converter = new HtmlToPdf();
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        PdfDocument doc = converter.ConvertHtmlString(htmlString);

        string filePath = Path.Combine(directoryPath, $"{invoice.InvoiceNumber}.pdf");
        doc.Save(filePath);
        doc.Close();
    }

    public OrderDto GetOrderWithClient(int orderId)
    {
        var order = _dbContext.Orders
            .Include(o => o.OrderDetail)
                .ThenInclude(od => od.OrderProducts)
                    .ThenInclude(op => op.Product)
            .Include(o => o.OrderDetail)
                .ThenInclude(od => od.DeliveryAddress)
            .Include(o => o.Client)
            .Include(o => o.Invoice);


        if (order == null)
        {
            throw new NotFoundException($"Order not found");
        }

        var orderDto = _mapper.Map<OrderDto>(order);

        return orderDto;
    }

}
