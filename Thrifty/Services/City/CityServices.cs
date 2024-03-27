using Microsoft.EntityFrameworkCore;
using Thrifty.Context;

namespace Thrifty.Services.City
{
    public class CityServices : ICityServices
    {
        private readonly ThriftyDbContext _dbContext;
        public CityServices(ThriftyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Models.City> Create(Models.City city)
        {
            _dbContext.Cities.Add(city);
            await _dbContext.SaveChangesAsync();
            return city;

        }

        public async Task<bool> Delete(int id)
        {
            var city = await Get(id);
            _dbContext.Cities.Remove(city);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Models.City> Get(int id)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(x => x.id == id);
            return city;
        }

        public async Task<List<Models.City>> GetAll()
        {
            var cities = await _dbContext.Cities.ToListAsync();
            return cities;
        }

        public async Task<Models.City> Update(Models.City City)
        {
            _dbContext.Cities.Update(City);
            await _dbContext.SaveChangesAsync();
            return City;
        }
    }
}
