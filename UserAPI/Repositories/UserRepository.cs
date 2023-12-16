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

        public UserRepository(UserContext userContext, IMapper mapper)
        {
            _userContext = userContext;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task CreateUser(User user)
        {
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            var dbUser = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (dbUser is null)
                return false;

            dbUser.Username = user.Username;
            dbUser.Name = user.Name;
            dbUser.Email = user.Email;

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
