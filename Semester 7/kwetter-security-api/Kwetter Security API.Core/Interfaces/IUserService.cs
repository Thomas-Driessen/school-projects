using System.Security.Claims;
using System.Security.Principal;
using Kwetter_Security_API.Dal.Models;

namespace Kwetter_Security_API.Core.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser(Guid id);
        public Task<User> CreateUser(ClaimsPrincipal claimsPrincipal);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(User user);
        public Task<UserFollow> GetFollowUser(UserFollow userFollow);
        public Task<UserFollow> CreateFollowUser(ClaimsPrincipal claimsPrincipal, UserFollow userFollow);
        public Task<UserFollow> UpdateFollowUser(UserFollow userFollowOriginal, UserFollow userFollow);
        public Task<UserFollow> DeleteFollowUser(UserFollow userFollow);
        public Task<List<UserFollow>> GetAllFollowersFromUser(Guid id);
        public Task<List<UserFollow>> GetAllUsersThatUserIsFollowing(Guid id);
    }
}