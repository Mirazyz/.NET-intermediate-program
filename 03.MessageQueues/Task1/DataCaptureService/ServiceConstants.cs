using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService
{
    public static class ServiceConstants
    {
        public const string Uri = "amqp://guest:guest@localhost:5672";
        public const string Exchange = "DataCaptureExchange";
        public const string FolderPath = @"C:\RabbitMQ\";
    }
}
