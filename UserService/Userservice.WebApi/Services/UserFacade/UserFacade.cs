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
            return await _users.GetAsync(user => user.Id == userId);
        }

        public async Task<User> Login(LoginRequest request)
        {
            return await _users.GetAsync(user => user.Email == request.Email && user.Password == request.Password);
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            
            await _users.InsertAsync(user);
            
            return user;
        }

    }
}
