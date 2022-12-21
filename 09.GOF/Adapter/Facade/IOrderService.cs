namespace Facade
{
    internal interface IOrderService
    {
        void PlaceOrder(string productId, int quantity, string email);
    }
}
