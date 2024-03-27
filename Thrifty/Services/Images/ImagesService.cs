using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thrifty.Context;
using Thrifty.Models;

namespace Thrifty.Services.Images
{
    public class ImagesService : IImagesService
    {
        private readonly ThriftyDbContext _dbContext;
        public ImagesService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<ItemImages>> GetAll(int itemId)
        {
            var Images = await _dbContext.ItemImages.Include(x => x.item).Where(x => x.itemId == itemId).ToListAsync();
            return Images;
        }

        public async Task<ItemImages> Get(int id , int itemId)
        {
            var Image = await _dbContext.ItemImages.Include(x => x.item).Where(x => x.itemId == itemId && x.id == id).FirstOrDefaultAsync();
            return Image;
        }

        public async Task<ItemImages> Add(ItemImages itemImages)
        {
            await _dbContext.ItemImages.AddAsync(itemImages);
            await _dbContext.SaveChangesAsync();
            return itemImages;
        }

        public async Task<ItemImages> Update(ItemImages itemImages)
        {
            _dbContext.ItemImages.Update(itemImages);
            await _dbContext.SaveChangesAsync();
            return itemImages;
        }

        public async Task<bool> UpdateSetMain(int id, int itemId)
        {
            var allImages = await _dbContext.ItemImages.Where(x => x.itemId == itemId).ToListAsync();
            allImages.ForEach(x => x.mainImage = false);
            allImages.Where(x => x.id == id).First().mainImage = true;
            _dbContext.ItemImages.UpdateRange(allImages);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var image = await _dbContext.ItemImages.FirstOrDefaultAsync(x => x.id == id);
            _dbContext.ItemImages.Remove(image);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        
    }
}
