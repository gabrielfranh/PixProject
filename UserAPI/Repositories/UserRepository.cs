using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserAPI.Context;
using UserAPI.DTOs;
using UserAPI.Models;
using UserAPI.Repositories.Interfaces;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;

        public UserRepository(UserContext userContext, IMapper mapper)
        {
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var dbUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == userDTO.Id);

            if (dbUser is null)
                return false;

            dbUser.Username = userDTO.Username;
            dbUser.Name = userDTO.Name;
            dbUser.Email = userDTO.Email;

            await _userContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            var user = (await _userContext.Users.FirstOrDefaultAsync(user => user.Id == id));

            if (user is null)
                return false;

            _userContext.Users.Remove(user);
            return true;
        }    
    }
}
