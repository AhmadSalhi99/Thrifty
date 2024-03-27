using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.ItemCategory
{
    public class ItemCategoryService : IItemCategoryService
    {

        private readonly ThriftyDbContext _dbContext;
        public ItemCategoryService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Models.ItemCategory> Create(Models.ItemCategory itemCategory)
        {
            _dbContext.ItemCategory.Add(itemCategory);
            await _dbContext.SaveChangesAsync();
            return itemCategory;

        }

        public async Task<bool> Delete(int id)
        {
            var itemCategory = await Get(id);
            _dbContext.Remove(itemCategory);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Models.ItemCategory> Get(int id)
        {
            var itemCategory = await _dbContext.ItemCategory.FirstOrDefaultAsync(x => x.id == id);
            return itemCategory;
        }

        public async Task<List<Models.ItemCategory>> GetAllInStock()
        {
            var itemCategores = await _dbContext.ItemCategory!.Include(x => x.Items!.Where(x => !x.outOfStock))!.ThenInclude(x => x.itemImages).ToListAsync();

            itemCategores.ForEach(x =>
            {
                x.Items?.ForEach(item =>
                {
                    item.mainImage = item?.itemImages?.Where(x => x.mainImage).FirstOrDefault();

                });
            });
            return itemCategores;
        }

        public async Task<List<Models.ItemCategory>> GetAll()
        {
            var itemCategores = await _dbContext.ItemCategory!.Include(x => x.Items)!.ThenInclude(x => x.itemImages).ToListAsync();

            itemCategores.ForEach(x =>
            {
                x.Items?.ForEach(item =>
                {
                    item.mainImage = item?.itemImages?.Where(x => x.mainImage).FirstOrDefault();

                });
            });
            return itemCategores;

        }

        public async Task<Models.ItemCategory> Update(Models.ItemCategory itemCategory)
        {
            _dbContext.ItemCategory.Update(itemCategory);
            await _dbContext.SaveChangesAsync();
            return itemCategory;
        }
    }
}
