using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter_Security_API.Dal.Context;
using Kwetter_Security_API.Dal.Interfaces;
using Kwetter_Security_API.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Kwetter_Security_API.Dal.Services
{
    public class UserAccess : IUserAccess
    {
        private readonly KwetterContext _kwetterContext;

        public UserAccess(KwetterContext kwetterContext)
        {
            _kwetterContext = kwetterContext;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _kwetterContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUser(User user)
        {
            await _kwetterContext.AddAsync(user);
            await _kwetterContext.SaveChangesAsync();
            return user;
        }

        public Task<User> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        #region Following functionality
        public Task<UserFollow> GetFollowUser(UserFollow userFollow)
        {
            throw new NotImplementedException();
        }

        public async Task<UserFollow> CreateFollowUser(UserFollow userFollow)
        {
            await _kwetterContext.AddAsync(userFollow);
            await _kwetterContext.SaveChangesAsync();
            return userFollow;
        }

        public async Task<UserFollow> UpdateFollowUser(UserFollow userFollowOriginal, UserFollow userFollow)
        {
            UserFollow result = await _kwetterContext.Following.FindAsync(userFollowOriginal);
            result = userFollow;
            await _kwetterContext.SaveChangesAsync();
            return result;
        }

        public async Task<UserFollow> DeleteFollowUser(UserFollow userFollow)
        {
            _kwetterContext.Following.Remove(userFollow);
            await _kwetterContext.SaveChangesAsync();
            return userFollow;
        }

        public async Task<List<UserFollow>> GetAllFollowersFromUser(Guid id)
        {
            return await _kwetterContext.Following.Where(uf => uf.UserId == id).ToListAsync();
        }

        public async Task<List<UserFollow>> GetAllUsersThatUserIsFollowing(Guid id)
        {
            return await _kwetterContext.Following.Where(uf => uf.UserId == id).ToListAsync();
        }
        #endregion
    }
}