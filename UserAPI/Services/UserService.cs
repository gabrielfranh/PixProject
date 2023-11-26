using UserAPI.DTOs;
using UserAPI.Repositories.Interfaces;
using UserAPI.Services.Interfaces;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user;
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            await _userRepository.CreateUser(userDTO);
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            return await _userRepository.UpdateUser(userDTO);
        }

        public async Task<bool> DeleteUserById(int id)
        {
            return await _userRepository.DeleteUserById(id);
        }
    }
}
