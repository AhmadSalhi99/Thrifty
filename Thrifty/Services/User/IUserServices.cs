namespace Thrifty.Services.User
{
    public interface IUserServices
    {

        public Task<Models.User> Get(int id);
        public Task<List<Models.User>> GetAll();
        public Task<List<Models.User>> GetAllNotCustomer();
        public Task<Models.User> Create(Models.User user);
        public Task<Models.User> Update(Models.User user);
        public Task<bool> Delete(int id);

        //---------------------------------------------------------------------------------------------------------------------------------------
        public Task<int> ValidateLogin(string email, string password);
        public Task<bool> ValidateEmail(string email);
    }
}
