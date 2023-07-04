using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter_Security_API.Dal.Models;

namespace Kwetter_Security_API.Dal.Interfaces
{
    public interface IUserAccess
    {
        public Task<User> GetUser(Guid id);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(User user);
        public Task<UserFollow> GetFollowUser(UserFollow userFollow);
        public Task<UserFollow> CreateFollowUser(UserFollow userFollow);
        public Task<UserFollow> UpdateFollowUser(UserFollow userFollowOriginal, UserFollow userFollow);
        public Task<UserFollow> DeleteFollowUser(UserFollow userFollow);
        public Task<List<UserFollow>> GetAllFollowersFromUser(Guid id);
        public Task<List<UserFollow>> GetAllUsersThatUserIsFollowing(Guid id);
    }
}