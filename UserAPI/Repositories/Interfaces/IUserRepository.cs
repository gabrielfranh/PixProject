using UserAPI.Models;

namespace UserAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(int id);
        public Task CreateUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUserById(int id);
    }
}
