namespace Facade
{
    internal class Payment
    {
        public int Id { get; set; }
        public decimal TotalDue { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
    }
}
