namespace Thrifty.Services.Role
{
    public interface IRoleService
    {
        public Task<List<Models.Role>> GetAll();
        public Task<Models.Role> Get(int id);
    }
}
