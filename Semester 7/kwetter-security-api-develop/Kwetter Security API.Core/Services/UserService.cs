using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Kwetter_Security_API.Core.Interfaces;
using Kwetter_Security_API.Dal.Interfaces;
using Kwetter_Security_API.Dal.Models;

namespace Kwetter_Security_API.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserAccess _userAccess;

        public UserService(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userAccess.GetUser(id);
        }

        public async Task<User> CreateUser(ClaimsPrincipal claimsPrincipal)
        {
            User newUser = new User()
            {
                Id = new Guid(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
                Username = claimsPrincipal.Identity.Name,
                Email = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                CreatedAt = DateTime.UtcNow,
                Disabled = false
            };
            
            return await _userAccess.CreateUser(newUser);
        }

        public async Task<User> CreateUser(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            return await _userAccess.CreateUser(user);
        }

        public Task<User> UpdateUser(User user) 
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteUser(User user) 
        {
            throw new NotImplementedException();
        }

        #region Following functionality
        public Task<UserFollow> GetFollowUser(UserFollow userFollow)
        {
            throw new NotImplementedException();
        }

        public async Task<UserFollow> CreateFollowUser(ClaimsPrincipal claimsPrincipal, UserFollow userFollow)
        {
            userFollow.UserId = new Guid(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            userFollow.FollowDate = DateTime.UtcNow;
            userFollow.IsFollowing = true;
            return await _userAccess.CreateFollowUser(userFollow);
        }

        public async Task<UserFollow> UpdateFollowUser(UserFollow userFollowOriginal, UserFollow userFollow)
        {
            return await _userAccess.UpdateFollowUser(userFollowOriginal, userFollow);
        }

        public async Task<UserFollow> DeleteFollowUser(UserFollow userFollow)
        {
            // gebruikt update functie voor de flag ipv record te verwijderen
            return await _userAccess.DeleteFollowUser(userFollow);
        }

        public async Task<List<UserFollow>> GetAllFollowersFromUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserFollow>> GetAllUsersThatUserIsFollowing(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}