using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Text;

public interface IInvoiceGenerator
{
    public byte[] GenerateInvoice(string htmlString);
    public string GetHTMLString(Invoice invoice);


}
public class InvoiceGenerator : IInvoiceGenerator
{
    private readonly IConverter _converter;

    public InvoiceGenerator(IConverter converter)
    {
        _converter = converter;
    }

    public string GetHTMLString(Invoice invoice)
    {
        string htmlContent = File.ReadAllText("wwwroot/templates/faktura.html");

        // Zastępowanie znaczników danymi
        htmlContent = htmlContent.Replace("[NumerFaktury]", invoice.InvoiceNumber);
        htmlContent = htmlContent.Replace("[DataFaktury]", invoice.InvoiceDate.ToString("dd-MM-yyyy"));
        htmlContent = htmlContent.Replace("[TerminPlatnosci]", invoice.DueDate.ToString("dd-MM-yyyy"));
        if(invoice.IsPaid) 
        {
            htmlContent = htmlContent.Replace("[StatusPlatnosci]", "Opłacona");
        }
        else
        {
            htmlContent = htmlContent.Replace("[StatusPlatnosci]", "Nieopłacona");
        }


        return htmlContent;
    }

    public byte[] GenerateInvoice(string htmlString)
    {
        var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            DocumentTitle = "Faktura",
            Out = null  // Zapisz do strumienia
        };

        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = htmlString, // Metoda generująca HTML na podstawie danych faktury
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { /* Ustawienia nagłówka, jeśli potrzebne */ },
            FooterSettings = { /* Ustawienia stopki, jeśli potrzebne */ }
        };

        var pdfDoc = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };

        return _converter.Convert(pdfDoc);
    }
}

