using Thrifty.Models;

namespace Thrifty.Services.Images
{
    public interface IImagesService
    {
        public Task<List<ItemImages>> GetAll(int itemId);
        public Task<ItemImages> Get(int id, int itemId);
        public Task<ItemImages> Add(ItemImages itemImages);
        public Task<ItemImages> Update(ItemImages itemImages);
        public Task<bool> UpdateSetMain(int id, int itemId);
        public Task<bool> Delete(int id);
    }
}
