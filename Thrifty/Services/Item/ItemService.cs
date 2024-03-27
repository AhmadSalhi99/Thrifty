using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.Item
{
    public class ItemService : IItemService
    {

        private readonly ThriftyDbContext _dbContext;
        public ItemService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Item>> GetAll(int userId = 0)
        {
            List<Models.Item> items = new List<Models.Item>();
            if (userId == 0)
            {
                items = await _dbContext.Items.Include(x => x.User)
                                              .Include(x => x.ItemType)
                                              .AsSplitQuery().AsNoTracking().ToListAsync();
            }
            else
            {
                items = await _dbContext.Items.Where(x => x.userId == userId)
                                          .Include(x => x.User)
                                          .Include(x => x.ItemType)
                                          .AsSplitQuery().AsNoTracking().ToListAsync();
            }
            return items;
        }
        public async Task<Models.Item> Get(int id)
        {
            var item = await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return item;
        }
        public async Task<List<Models.Item>> GetCartItems(List<int> itemsId)
        {

            var items = await _dbContext.Items.Include(x => x.ItemType).Include(x => x.itemImages).Where(x => itemsId.Contains(x.id) && x.outOfStock == false).ToListAsync();
            items.ForEach(item =>
            {
                item.mainImage = item?.itemImages?.Where(x => x.mainImage).FirstOrDefault();
            });
            return items;
        }
        public async Task<List<int>> GetSoldItemsId(string orderNumber)
        {
            var orderIds = await _dbContext.OrderDetails.Where(x => x.OrderNumber == orderNumber).Select(x => x.ItemId).ToListAsync();
            return orderIds;
        }
        public async Task<Models.Item> GetWithImages(int id)
        {
            var item = await _dbContext.Items.Include(x => x.ItemType).Include(x => x.itemImages).AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            item.mainImage = item.itemImages.Where(x => x.mainImage).FirstOrDefault();
            return item;
        }
        public async Task<Models.Item> Create(Models.Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        public async Task<Models.Item> Update(Models.Item item)
        {
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        public async Task<bool> SetItemToOutOfStock(List<int> itemIds)
        {
            var items = await _dbContext.Items.Where(x => itemIds.Contains(x.id)).ToListAsync();
            if (items != null && items.Count > 0)
            {
                items.ForEach(x => x.outOfStock = true);
                _dbContext.Items.UpdateRange(items);
            }
            else
            {
                return true;
            }
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> Delete(int Id)
        {
            var item = await Get(Id);
            _dbContext.Remove(item);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

    }
}
