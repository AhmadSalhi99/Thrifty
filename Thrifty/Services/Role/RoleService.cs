using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.Role
{
    public class RoleService : IRoleService
    {

        private readonly ThriftyDbContext _dbContext;
        public RoleService(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Models.Role> Get(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.id == id);
            return role;
        }

        public async Task<List<Models.Role>> GetAll()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return roles;
        }
    }
}
