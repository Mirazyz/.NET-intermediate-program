namespace CatalogService.Domain.Exceptions
{
    public class NotFoundDbException : Exception
    {
        public NotFoundDbException(string? message)
            : base(message)
        {
        }
    }
}
