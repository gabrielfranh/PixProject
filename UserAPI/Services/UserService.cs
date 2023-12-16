using AutoMapper;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Security.Cryptography;
using System.Text;
using UserAPI.DTOs;
using UserAPI.Models;
using UserAPI.Repositories.Interfaces;
using UserAPI.Services.Interfaces;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            var salt = GenerateSalt();
            userDTO.Salt = Convert.ToBase64String(salt);

            var passHash = GeneratePasswordHash(userDTO.Password, salt);
            userDTO.Hash = passHash;

            var user = _mapper.Map<User>(userDTO);

            await _userRepository.CreateUser(user);

            PublishUserCreateMessage(userDTO);
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            return await _userRepository.UpdateUser(user);
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

        private static void PublishUserCreateMessage(UserDTO userDTO)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "my-queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = userDTO.ToString();
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "my-exchange",
                                 routingKey: "user_create",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
