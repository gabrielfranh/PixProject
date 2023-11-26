using System.Security.Cryptography;
using System.Text;
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
            var salt = GenerateSalt();
            userDTO.Salt = Convert.ToBase64String(salt);

            var passHash = GeneratePasswordHash(userDTO.Password, salt);
            userDTO.Hash = passHash;

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

        private static byte[] GenerateSalt()
        {
            var salt = new byte[32];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string GeneratePasswordHash(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordBytes);
            passwordWithSaltBytes.AddRange(salt);

            var digestBytes = SHA512.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            return Convert.ToBase64String(digestBytes);
        }
    }
}
