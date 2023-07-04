using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using minecraft_panel_api.Authorisation.DAL.Models;

namespace minecraft_panel_api.Authorisation.DAL.Interfaces
{
    public interface IUserDbAccess
    {
        public Task<User> GetUserById(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<IdentityUser> GetIdentityUser(IdentityUser identityUser);
        public Task<IdentityUser> GetIdentityUserById(string id);
        public Task<IdentityUser> GetIdentityUserByName(string username);
        public Task<IdentityUser> GetIdentityUserByEmail(string email);
        public Task<IdentityRole> GetIdentityRoleById(IdentityRole identityRole);
        public Task<IdentityResult> RegisterIdentityUser(IdentityUser identityUser, string password);
        public Task<IdentityResult> RegisterUser(IdentityUser identityUser, User user);
        public Task<IList<string>> GetUserRoles(IdentityUser identityUser);
        public Task<IdentityResult> SetUserRole(IdentityUser identityUser, IdentityRole identityRole);
        public Task<IdentityResult> CreateRole(IdentityRole identityRole);
        public Task<int> UpdateUser(User user);
    }
}