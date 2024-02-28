using Userservice.Models.Entities;
using Userservice.Models.Requests;

namespace Userservice.WebApi.Services.UserFacade
{
    public interface IUserFacade
    {
        Task<User> Register(RegisterRequest request);
        Task<User> Login(LoginRequest request);
        Task<User> GetUserById(string userId);
    }
}
