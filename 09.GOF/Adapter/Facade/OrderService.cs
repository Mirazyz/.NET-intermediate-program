namespace Facade
{
    internal class OrderService : IOrderService
    {
        private readonly IPaymentService paymentService;
        private readonly IInvoiceService invoiceService;
        private readonly IProductCatalog productCatalog;

        public OrderService(IPaymentService paymentService, IInvoiceService invoiceService, IProductCatalog productCatalog)
        {
            this.paymentService = paymentService;
            this.invoiceService = invoiceService;
            this.productCatalog = productCatalog;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            var product = productCatalog.GetProductDetails(productId);

            if(product == null)
            {
                return;
            }

            var totalPrice = product.Price * quantity;

            var payment = new Payment()
            {
                TotalDue = totalPrice,
                DestinationAccount = "some account",
                SourceAccount = "some account"
            };

            var paymentSuccess = paymentService.MakePayment(payment);

            if (paymentSuccess)
            {
                throw new Exception("There was an error processing payment.");
            }

            var invoice = new Invoice()
            {
                Details = "Invoice details.",
                TotalSum = totalPrice,
                Email = email
            };

            invoiceService.SendInvoice(invoice);
        }
    }
}
