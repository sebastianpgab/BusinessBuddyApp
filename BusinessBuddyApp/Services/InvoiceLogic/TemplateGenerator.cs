using System.Text;

namespace BusinessBuddyApp.Services.InvoiceLogic
{
    public class TemplateGenerator
    {
        public TemplateGenerator()
        {

        }
        public string GetHTMLString()
        {
            var sb = new StringBuilder();
            string htmlFilePath = "wwwroot/templates/faktura.html";
            string htmlContent = File.ReadAllText(htmlFilePath);


            sb.Append(htmlContent);
            return sb.ToString();

        }
    }
}
