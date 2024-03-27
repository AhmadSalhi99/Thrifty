using Microsoft.AspNetCore.Http;

namespace Thrifty.Authentication
{
    public static class AuthRole
    {

        public static HttpContext HttpContext
        {
            get
            {
                return _httpContextAccessor!.HttpContext!;
            }
        }

        private static IHttpContextAccessor? _httpContextAccessor = null;
        internal static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
       


        public static bool IsUserInRoll(string strRoll)
        {
            try
            {
                HttpContext context = HttpContext;
                ISession session = context.Session;
                var rollId = session.GetInt32("LogedUserRole");
                if (Enum.TryParse(typeof(roll) , strRoll, out object? EnumRollId))
                {
                    if((int)EnumRollId! == rollId)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                
            }
        }

        

        public enum roll
        {
            Seller = 1,
            Admin = 2,
            Customer = 3,
        }

    }
}
