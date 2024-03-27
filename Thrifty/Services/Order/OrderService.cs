using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.Order
{
    public class OrderService : IOrderService
    {
        enum OrderStatus
        {
            Pending = 1,
            Accepted = 2,
            OnTheWay = 3,
            Delivered = 4,
            Canceled = 5,
        }
        private readonly ThriftyDbContext _dbContext;
        public OrderService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Models.Order> AddOrderAsync(Models.Order Order)
        {
            await _dbContext.Orders.AddAsync(Order);
            await _dbContext.SaveChangesAsync();
            return Order;
        }
        public async Task<Models.Order> AcceptOrderAsync(string orderNumber)
        {
            var order = await _dbContext.Orders.Where(x => x.OrderNumber == orderNumber).FirstAsync();
            order.statusId = (int)OrderStatus.Accepted;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Models.Order> OnTheWayOrderAsync(string orderNumber)
        {
            var order = await _dbContext.Orders.Where(x => x.OrderNumber == orderNumber).FirstAsync();
            order.statusId = (int)OrderStatus.OnTheWay;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Models.Order> DeliveredOrderAsync(string orderNumber)
        {
            var order = await _dbContext.Orders.Where(x => x.OrderNumber == orderNumber).FirstAsync();
            order.statusId = (int)OrderStatus.Delivered;
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
