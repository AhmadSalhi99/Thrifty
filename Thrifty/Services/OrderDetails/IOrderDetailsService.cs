namespace Thrifty.Services.OrderDetails
{
    public interface IOrderDetailsService
    {
        public Task<List<Models.OrderDetails>> AddOrderDetailsRangeAsync(List<Models.OrderDetails> orderDetails);
        public Task<List<Models.OrderDetails>> GetOrderDetailsRangeAsync(string orderNumber);


        public Task<List<Models.OrderDetails>> GetUserOrders(int userId);
        public Task<List<Models.OrderDetails>> GetAllPending(int userId = 0);
        public Task<List<Models.OrderDetails>> GetAllAccepted(int userId = 0);
        public Task<List<Models.OrderDetails>> GetAllOnTheWay(int userId = 0);
        public Task<List<Models.OrderDetails>> GetAllDelivered(int userId = 0);
        public Task<List<Models.OrderDetails>> GetAllRejected(int userId = 0);


        public Task<Models.OrderDetails> AcceptOrderAsync(int id);
        public Task<Models.OrderDetails> OnTheWayOrderAsync(int id);
        public Task<Models.OrderDetails> DeliveredOrderAsync(int id);
        public Task<Models.OrderDetails> RejectOrderAsync(int id);


        public Task<bool> CheckIfAllDetailsAccepted(string orderNumber);
    }
}
