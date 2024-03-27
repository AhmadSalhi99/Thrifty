namespace Thrifty.Services.ItemCategory
{
    public interface IItemCategoryService
    {
        public Task<List<Models.ItemCategory>> GetAll();
        public Task<List<Models.ItemCategory>> GetAllInStock();
        public Task<Models.ItemCategory> Get(int id);
        public Task<Models.ItemCategory> Create(Models.ItemCategory itemCategory);
        public Task<Models.ItemCategory> Update(Models.ItemCategory itemCategory);
        public Task<bool> Delete(int id);
    }
}
