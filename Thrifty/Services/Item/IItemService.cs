using System.Runtime.CompilerServices;

namespace Thrifty.Services.Item
{
    public interface IItemService
    {
        public Task<List<Models.Item>> GetAll(int userId = 0);
        public Task<Models.Item> Get(int Id);
        public Task<List<Models.Item>> GetCartItems(List<int> itemsId);
        public Task<List<int>> GetSoldItemsId(string orderNumber);
        public Task<Models.Item> GetWithImages(int id);
        public Task<Models.Item> Create(Models.Item item);
        public Task<Models.Item> Update(Models.Item item);
        public Task<bool> SetItemToOutOfStock(List<int> itemIds);
        public Task<bool> Delete(int Id);
    }
}
