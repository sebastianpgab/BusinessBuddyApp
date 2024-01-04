using BusinessBuddyApp.Entities;
using System.IO;
using System.Text;
using SelectPdf; // Dodaj tę przestrzeń nazw

public interface IInvoiceGenerator
{
    void GenerateInvoice(Invoice invoice, string directoryPath);
    string GetHTMLString(Invoice invoice);
}

public class InvoiceGenerator : IInvoiceGenerator
{
    public InvoiceGenerator()
    {
    }

    public string GetHTMLString(Invoice invoice)
    {
        string htmlContent = File.ReadAllText("wwwroot/templates/faktura.html");

        htmlContent = htmlContent.Replace("[NumerFaktury]", invoice.InvoiceNumber);
        htmlContent = htmlContent.Replace("[DataFaktury]", invoice.InvoiceDate.ToString("dd-MM-yyyy"));
        htmlContent = htmlContent.Replace("[TerminPlatnosci]", invoice.DueDate.ToString("dd-MM-yyyy"));
        htmlContent = htmlContent.Replace("[StatusPlatnosci]", invoice.IsPaid ? "Opłacona" : "Nieopłacona");

        return htmlContent;
    }

    public void GenerateInvoice(Invoice invoice, string directoryPath)
    {
        string htmlString = GetHTMLString(invoice);

        HtmlToPdf converter = new HtmlToPdf();
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
        // Dodatkowe opcje konwersji

        PdfDocument doc = converter.ConvertHtmlString(htmlString);

        string filePath = Path.Combine(directoryPath, $"{invoice.InvoiceNumber}.pdf");
        doc.Save(filePath);
        doc.Close();
    }

}
