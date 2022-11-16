using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace BrokerService
{
    public class Client
    {
        public static void Subscribe(string queue, string routingKey)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue, true, false, false);
            channel.QueueBind(queue, ServiceConstants.Exchange, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine($"{eventArgs.RoutingKey}: {msg}");
            };

            channel.BasicConsume(queue, true, consumer);

            Console.ReadLine();

            channel.Close();
            connection.Close();
        }
    }
}
