using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Authorisation.DAL.Context;
using minecraft_panel_api.Authorisation.DAL.Interfaces;
using minecraft_panel_api.Authorisation.DAL.Models;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation.DAL.Classes
{
    public class UserDbAccess : IUserDbAccess
    {
        private readonly MinecraftPluginContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserDbAccess(MinecraftPluginContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User> GetUserById(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(find => find.Id == user.Id);
        }
        
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(find => find.Email == email);
        }

        public async Task<IdentityUser> GetIdentityUser(IdentityUser identityUser)
        {
            return await _userManager.FindByIdAsync(identityUser.Id);
        }
        
        public async Task<IdentityUser> GetIdentityUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        
        public async Task<IdentityUser> GetIdentityUserByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        
        public async Task<IdentityUser> GetIdentityUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityRole> GetIdentityRoleById(IdentityRole identityRole)
        {
            return await _roleManager.FindByIdAsync(identityRole.Id);
        }

        public async Task<IdentityResult> RegisterIdentityUser(IdentityUser identityUser, string password)
        {
            IdentityResult userCreationResult = await _userManager.CreateAsync(identityUser, password);
            return userCreationResult;
        }
        
        public async Task<IdentityResult> RegisterUser(IdentityUser identityUser, User user)
        {
            await _context.Users.AddAsync(user);
            int changedRows = await _context.SaveChangesAsync();
            
            if (changedRows <= 0) 
                return IdentityResult.Failed();
            
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetUserRole(IdentityUser identityUser, IdentityRole identityRole)
        {
            return await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
        }

        public async Task<IList<string>> GetUserRoles(IdentityUser identityUser)
        {
            return await _userManager.GetRolesAsync(identityUser);
        }

        public async Task<IdentityResult> CreateRole(IdentityRole identityRole)
        {
            return await _roleManager.CreateAsync(identityRole);
        }

        public async Task<int> UpdateUser(User user)
        {
            _context.Users.Update(user);
            int result = await _context.SaveChangesAsync();
            return result;
        }
    }
}