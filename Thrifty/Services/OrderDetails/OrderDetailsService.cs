using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.OrderDetails
{
    public class OrderDetailsService : IOrderDetailsService
    {
        enum OrderStatus
        {
            Pending = 1,
            Accepted = 2,
            OnTheWay = 3,
            Delivered = 4,
            Canceled = 5,
            Rejected = 6,
        }

        private readonly ThriftyDbContext _dbContext;

        public OrderDetailsService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.OrderDetails>> GetOrderDetailsRangeAsync(string orderNumber)
        {
            var orderDetails = await _dbContext.OrderDetails.Include(x => x.Item).ThenInclude(x => x.ItemType).Where(x => x.OrderNumber == orderNumber).ToListAsync();
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> AddOrderDetailsRangeAsync(List<Models.OrderDetails> orderDetails)
        {
            try
            {
                await _dbContext.OrderDetails.AddRangeAsync(orderDetails);
                await _dbContext.SaveChangesAsync();
                return orderDetails;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<Models.OrderDetails>> GetUserOrders(int userId)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.buyerId == userId)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .ThenInclude(x => x.ItemType)
                                                     .Include(x => x.Item)
                                                     .ThenInclude(x => x.itemImages)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

            orderDetails.ForEach(order =>
            {
                order.Item!.mainImage = order.Item.itemImages!.Where(x => x.mainImage == true).FirstOrDefault();
            });
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> GetAllPending(int userId = 0)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.statusId == (int)OrderStatus.Pending)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .Where(x => x.Item.userId == userId || userId == 0)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> GetAllAccepted(int userId = 0)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.statusId == (int)OrderStatus.Accepted)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .Where(x => x.Item.userId == userId || userId == 0)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> GetAllOnTheWay(int userId = 0)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.statusId == (int)OrderStatus.OnTheWay)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .Where(x => x.Item.userId == userId || userId == 0)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> GetAllDelivered(int userId = 0)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.statusId == (int)OrderStatus.Delivered)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .Where(x => x.Item.userId == userId || userId == 0)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return orderDetails;
        }
        public async Task<List<Models.OrderDetails>> GetAllRejected(int userId = 0)
        {
            List<Models.OrderDetails> orderDetails;
            orderDetails = await _dbContext.OrderDetails.Where(x => x.statusId == (int)OrderStatus.Rejected)
                                                     .Include(x => x.User)
                                                     .Include(x => x.OrderStatus)
                                                     .Include(x => x.Item)
                                                     .Where(x => x.Item.userId == userId || userId == 0)
                                                     .AsSplitQuery().AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return orderDetails;
        }


        public async Task<Models.OrderDetails> AcceptOrderAsync(int id)
        {
            var order = await _dbContext.OrderDetails.Where(x => x.Id == id).FirstAsync();
            order.statusId = (int)OrderStatus.Accepted;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Models.OrderDetails> OnTheWayOrderAsync(int id)
        {
            var order = await _dbContext.OrderDetails.Where(x => x.Id == id).FirstAsync();
            order.statusId = (int)OrderStatus.OnTheWay;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Models.OrderDetails> DeliveredOrderAsync(int id)
        {
            var order = await _dbContext.OrderDetails.Where(x => x.Id == id).FirstAsync();
            order.statusId = (int)OrderStatus.Delivered;
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public async Task<Models.OrderDetails> RejectOrderAsync(int id)
        {
            var order = await _dbContext.OrderDetails.Where(x => x.Id == id).FirstAsync();
            order.statusId = (int)OrderStatus.Rejected;
            await _dbContext.SaveChangesAsync();
            return order;
        }


        

        public async Task<bool> CheckIfAllDetailsAccepted(string orderNumber)
        {
            var done = true;
            var allDetails = await _dbContext.OrderDetails.Where(x => x.OrderNumber == orderNumber).ToListAsync();
            allDetails.ForEach(x =>
            {
                if (x.statusId == (int)OrderStatus.Pending)
                {
                    done = false;
                }
            });
            return done;
        }
    }
}