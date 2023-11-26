using UserAPI.DTOs;

namespace UserAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDTO> GetUserById(int id);
        public Task CreateUser(UserDTO userDTO);
        public Task<bool> UpdateUser(UserDTO userDTO);
        public Task<bool> DeleteUserById(int id);
    }
}
