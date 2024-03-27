using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.User
{
    public class UserServices : IUserServices
    {

        private readonly ThriftyDbContext _dbContext;
        public UserServices(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public async Task<Models.User> Get(int id)
        {
            try
            {
                var user = await _dbContext.Users.Include(x => x.role).AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Models.User>> GetAll()
        {
            try
            {
                var user = await _dbContext.Users.Include(x => x.role).AsNoTracking().ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<Models.User>> GetAllNotCustomer()
        {
            try
            {
                var user = await _dbContext.Users.Include(x => x.role).Where(x => x.roleId != 3).AsNoTracking().ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Models.User> Create(Models.User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public async Task<Models.User> Update(Models.User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await Get(id);
                _dbContext.Users.Remove(user);
                var deleted = await _dbContext.SaveChangesAsync();
                return deleted > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }


        //---------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> ValidateLogin(string email, string password)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.email == email && x.password == password);
                if (user == null)
                { 
                    return 0;
                }
                return user.id;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }


        public async Task<bool> ValidateEmail(string email)
        {
            try
            {
                var isExist = await _dbContext.Users.Where(x => x.email == email).AnyAsync();
                return isExist;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
