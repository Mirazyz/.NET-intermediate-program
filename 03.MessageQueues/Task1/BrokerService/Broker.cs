using FileHandlerService;
using RabbitMQ.Client;

namespace BrokerService
{
    public class Broker
    {
        private static readonly List<string> allowedFileExtensions = new()
        {
            ".txt",
            ".pdf",
            ".jpg"
        };


        public static void SendFile(string path)
        {
            var file = FileService.ConvertFileToBytes(path);
            string fileExt = Path.GetExtension(path);

            if (!allowedFileExtensions.Contains(fileExt))
            {
                Console.WriteLine($"File extension: {fileExt} is not allowed and won't be sent to the Subscribers.");
                return;
            }

            if (file is null) return;

            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicPublish(ServiceConstants.Exchange, fileExt, null, file);

            channel.Close();
            connection.Close();
        }

        public static void CreateExchange()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(ServiceConstants.Exchange, ExchangeType.Direct, true);

            channel.Close();
            connection.Close();
        }
    }
}
