using FileHandlerService;
using RabbitMQ.Client;

namespace BrokerService
{
    public class Broker
    {
        public static void SendFile(string path)
        {
            var file = FileService.ConvertFileToBytes(path);

            if (file is null) return;

            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicPublish(ServiceConstants.Exchange, "pdfProcessingQueue", null, file);

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
