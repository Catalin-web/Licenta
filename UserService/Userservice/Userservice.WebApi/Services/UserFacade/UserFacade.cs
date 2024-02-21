using Userservice.DataStore.UserProvider;
using Userservice.Models.Entities;
using Userservice.Models.Requests;

namespace Userservice.WebApi.Services.UserFacade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserDataProvider _users;

        public UserFacade(IUserDataProvider users)
        {
            _users = users;
        }

        public async Task<User> GetUserById(string userId)
        {
            try
            {
                return await _users.GetAsync(user => user.Id == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<User> Login(LoginRequest request)
        {
            try
            {
                return await _users.GetAsync(user => user.Email == request.Email && user.Password == request.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                Password = request.Password
            };
            try
            {
                await _users.InsertAsync(user);
                return await _users.GetAsync(user => user.Email == request.Email && user.Password == request.Password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
