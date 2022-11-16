using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlerService
{
    public class ErrorHandler
    {
        public static void PrintException(Exception? ex)
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
