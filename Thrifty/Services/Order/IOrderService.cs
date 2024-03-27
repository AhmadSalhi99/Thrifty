namespace Thrifty.Services.Order
{
    public interface IOrderService
    {
        public Task<Models.Order> AddOrderAsync(Models.Order Order);
        public Task<Models.Order> AcceptOrderAsync(string orderNumber);
        public Task<Models.Order> OnTheWayOrderAsync(string orderNumber);
        public Task<Models.Order> DeliveredOrderAsync(string orderNumber);
    }
}
