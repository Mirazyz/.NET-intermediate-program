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
                Filter = "*.*"
            };

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
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
    }
}