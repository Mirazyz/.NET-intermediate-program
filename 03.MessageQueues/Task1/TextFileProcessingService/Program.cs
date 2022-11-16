using BrokerService;

namespace TextFileProcessingService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Text file processing service has started...");

            Client.Subscribe("txtProcessingQueue", ".txt");
        }
    }
}