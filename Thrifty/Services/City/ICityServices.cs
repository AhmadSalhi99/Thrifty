namespace Thrifty.Services.City
{
    public interface ICityServices
    {

        public Task<List<Models.City>> GetAll();
        public Task<Models.City> Get(int id);
        public Task<Models.City> Create(Models.City City);
        public Task<Models.City> Update(Models.City City);
        public Task<bool> Delete(int id);
    }
}
