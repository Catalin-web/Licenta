using System.Linq.Expressions;
using Userservice.Models.Entities;

namespace Userservice.DataStore.UserProvider
{
    public interface IUserDataProvider
    {
        Task InsertAsync(User user);
        Task<User> GetAsync(Expression<Func<User, bool>> match);
    }
}
