using Facade;

IInvoiceService invoiceService = new InvoiceService();
IProductCatalog productCatalog = new ProductCatalog();
IPaymentService paymentService = new PaymentService();

OrderService orderService = new OrderService(paymentService, invoiceService, productCatalog);

orderService.PlaceOrder("15", 20, "john@mail.com");