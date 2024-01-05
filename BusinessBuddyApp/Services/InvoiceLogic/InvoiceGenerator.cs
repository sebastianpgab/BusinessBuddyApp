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



        if(true)
        {

            string htmlContent = File.ReadAllText("wwwroot/templates/faktura.html");

           /* htmlContent = htmlContent.Replace("[ImieNabywcy]", result.Client.FirstName);
            htmlContent = htmlContent.Replace("[NazwiskoNabywcy]", result.Client.LastName);
            htmlContent = htmlContent.Replace("[NumerTelNabywcy]", result.Client.PhoneNumber);
            htmlContent = htmlContent.Replace("[EmailNabywcy]", result.Client.Email);
            htmlContent = htmlContent.Replace("[ZamowienieID]", result.Id.ToString());*/

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
        var order = _dbContext.Orders.Include(c => c.Client);


        if (order == null)
        {
            return null;
        }

        var orderDto = _mapper.Map<OrderDto>(order);

        return orderDto;
    }

}
