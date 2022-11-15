using RabbitMQ.Client;

namespace DataCaptureService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Processing service has started...");

            Watch();

            Console.ReadLine();
        }

        private static void Watch()
        {
            FileSystemWatcher watcher = new()
            {
                Path = ServiceConstants.FolderPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.txt*",
                EnableRaisingEvents = true
            };

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Error += OnError;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed) return;

            Console.WriteLine($"Changed: {e.FullPath}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string message = $"Created: {e.FullPath}";

            Console.WriteLine(message);
        }

        private static void SendFile(byte[] file)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(ServiceConstants.Uri);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicPublish(ServiceConstants.Exchange, "", null, file);

            channel.Close();
            connection.Close();
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stacktrace: {ex.StackTrace}");
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}