namespace FileHandlerService
{
    public static class FileService
    {
        public static byte[] ConvertFileToBytes(string path = "")
        {
            if (!File.Exists(path))
                ErrorHandler.PrintException(
                    new FileNotFoundException($"File does not exist in given directory: {path}."));

            using FileStream fs = File.OpenRead(path);

            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            return bytes;
        }

        public static void ConvertBytesToFile(byte[] fileBytes)
        {
        }
    }
}
