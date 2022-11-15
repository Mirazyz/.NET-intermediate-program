using RabbitMQ.Client;

namespace DataCaptureService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Processing service has started...");

            Watch();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static void Watch()
        {
            FileSystemWatcher watcher = new()
            {
                Path = ServiceConstants.FolderPath,
                EnableRaisingEvents = true,
                Filter = "*.*",
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size
            };

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            watcher.Error += OnError;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed) return;

            Console.WriteLine($"Changed: {e.FullPath}. Sending it over broker.");

            Broker.SendFile(e.FullPath);
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Created: {e.FullPath}. Sending it over broker.");
            
            Broker.SendFile(e.FullPath);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Deleted: {e.FullPath}. Sending it over broker.");

            Broker.SendFile(e.FullPath);
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            ErrorHandlerService.PrintException(e.GetException());
    }
}