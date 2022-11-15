using RabbitMQ.Client;

namespace DataCaptureService
{
    internal static class Broker
    {
        internal static void SendFile(byte[] file)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicPublish(ServiceConstants.Exchange, "", null, file);

            channel.Close();
            connection.Close();
        }
    }
}
