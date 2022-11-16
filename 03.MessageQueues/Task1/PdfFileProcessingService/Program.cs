using BrokerService;

namespace PdfFileProcessingService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pdf processing service has started...");
            Client.Subscribe("pdfFileProcessingQueue", ".pdf", ".pdf");
        }
    }
}