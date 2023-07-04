using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using minecraft_panel_api.Authorisation.DAL.Context;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation.DAL.Classes
{
    public class IdentityServiceAccess : IIdentityServiceAccess
    {
        private readonly MinecraftPluginContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityServiceAccess(MinecraftPluginContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> SignInUserWithPassword(string username, string password, bool isPersistant,
            bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(username, password, isPersistant, lockoutOnFailure);
        }

        public async Task<IdentityResult> ResetPasswordWithEmail(IdentityUser identityUser, string token, string password)
        {
            IdentityResult result = await _userManager.ResetPasswordAsync(identityUser, token, password);
            return result;
        }
        
        public async Task<IdentityResult> ResetPassword(IdentityUser identityUser, string token, string password)
        {
            IdentityResult result = await _userManager.ResetPasswordAsync(identityUser, token, password);
            return result;
        }
        
        public async Task<IdentityResult> ResetEmailWithEmail(IdentityUser identityUser, string email, string token)
        {
            IdentityResult result = await _userManager.ChangeEmailAsync(identityUser, email, token);
            return result;
        }
        
        public async Task<string> GenerateEmailResetToken(IdentityUser identityUser, string newEmail)
        {
            string result = await _userManager.GenerateChangeEmailTokenAsync(identityUser, newEmail);
            return result;
        }
        
        public async Task<string> GeneratePasswordResetToken(IdentityUser identityUser)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
            return resetToken;
        }
        
        public async Task<string> GenerateEmailConfirmationToken(IdentityUser identityUser)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            return confirmationToken;
        }
        
        public async Task<IdentityResult> ConfirmEmail(IdentityUser identityUser, string token)
        {
            IdentityResult result = await _userManager.ConfirmEmailAsync(identityUser, token);
            return result;
        }
    }
}