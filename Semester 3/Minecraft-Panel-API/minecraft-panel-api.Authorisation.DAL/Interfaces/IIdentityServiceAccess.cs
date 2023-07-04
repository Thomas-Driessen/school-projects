using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace minecraft_panel_api.Authorisation.DAL.Interfaces
{
    public interface IIdentityServiceAccess
    {
        public Task<SignInResult> SignInUserWithPassword(string username, string password, bool isPersistant, bool lockoutOnFailure);
        public Task<IdentityResult> ResetPasswordWithEmail(IdentityUser identityUser, string token, string password);
        public Task<IdentityResult> ResetPassword(IdentityUser identityUser, string token, string password);
        public Task<IdentityResult> ResetEmailWithEmail(IdentityUser identityUser, string email, string token);
        public Task<string> GenerateEmailResetToken(IdentityUser identityUser, string newEmail);
        public Task<string> GeneratePasswordResetToken(IdentityUser identityUser);
        public Task<string> GenerateEmailConfirmationToken(IdentityUser identityUser);
        public Task<IdentityResult> ConfirmEmail(IdentityUser identityUser, string token);
    }
}