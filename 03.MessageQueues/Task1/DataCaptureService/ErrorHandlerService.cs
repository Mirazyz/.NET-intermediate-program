namespace DataCaptureService
{
    public static class ErrorHandlerService
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
