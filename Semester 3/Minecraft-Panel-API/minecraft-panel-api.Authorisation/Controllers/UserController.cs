using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Authorisation.DAL.Context;
using minecraft_panel_api.Authorisation.DAL.Models;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("/api/main/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly MinecraftPluginContext _context;
        private readonly IUserDbAccess _userDb;
        private readonly IEmailService _emailService;
        private readonly IIdentityServiceAccess _identityServiceAccess;

        public UserController(MinecraftPluginContext context, IUserDbAccess userDb, IEmailService emailService, IIdentityServiceAccess identityServiceAccess)
        {
            _context = context;
            _userDb = userDb;
            _emailService = emailService;
            _identityServiceAccess = identityServiceAccess;
        }
        
        [HttpPost("getUserData")]
        public async Task<User> GetUserData(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == user.Name);
        }

        [HttpPost("linkMCAccount")]
        public async Task<User> LinkMCAccount(User user)
        {
            User foundUser = await _userDb.GetUserById(user);
            
            return user;
        }

        [HttpPost("registerAccount")]
        public async Task<ActionResult<IdentityResult>> RegisterAccount(RegisterModel registerModel)
        {
            IdentityResult identityResult = await _userDb.RegisterIdentityUser(registerModel.UserDetails, registerModel.Password);

            if (!identityResult.Succeeded)
                return BadRequest(identityResult);

            User newUser = new User
            {
                Email = registerModel.UserDetails.Email,
                Name = registerModel.UserDetails.UserName,
                RegisteredAccount = registerModel.UserDetails
            };
            
            IdentityResult userResult = await _userDb.RegisterUser(registerModel.UserDetails, newUser);

            if (!userResult.Succeeded)
                return BadRequest(userResult);
            
            return Ok(newUser);
        }

        [HttpPost("setUserRole")]
        public async Task<ActionResult<IdentityResult>> SetUserRole([FromBody] RoleSetModel roleSetModel)
        {
            IdentityUser foundIdentityUser = await _userDb.GetIdentityUser(roleSetModel.User);
            IdentityRole foundIdentityRole = await _userDb.GetIdentityRoleById(roleSetModel.Role);

            if (foundIdentityRole == null || foundIdentityUser == null)
                return NotFound("Didn't find the user or role");

            IdentityResult roleSetResult = await _userDb.SetUserRole(foundIdentityUser, foundIdentityRole);

            if (!roleSetResult.Succeeded)
                return BadRequest(roleSetResult.Errors);

            return Ok("Role set!");
        }

        [Authorize (Roles = "Admin")]
        [HttpPost("createRole")]
        public async Task<ActionResult<IdentityResult>> CreateRole([FromBody] IdentityRole identityRole)
        {
            IdentityResult roleCreationResult = await _userDb.CreateRole(identityRole);
            
            if (!roleCreationResult.Succeeded)
                return BadRequest(roleCreationResult.Errors);

            return Ok("Role created!");
        }

        [HttpPost("resetEmail")]
        public async Task<ActionResult> ChangeEmailWithoutEmail(EmailResetModel emailResetModel)
        {
            if (String.IsNullOrWhiteSpace(emailResetModel.NewEmail))
                return BadRequest("The email is empty!");

            string userToken = HttpContext.User.Identity.GetSubjectId();
            
            if (String.IsNullOrWhiteSpace(userToken))
                return BadRequest("No user found in the request.");

            IdentityUser identityUser = await _userDb.GetIdentityUserById(userToken);

            if (identityUser == null)
                return NotFound("The user was not found!");

            User normalUser = await _userDb.GetUserByEmail(identityUser.Email);
            
            if (normalUser == null)
                return NotFound("The user was not found!");

            if (!identityUser.EmailConfirmed)
                return BadRequest("Your email is not confirmed!");

            if (identityUser.Email != emailResetModel.CurrentEmail)
                return BadRequest("Your currently entered email does not match the account's email!");
                
            string resetToken = await _identityServiceAccess.GenerateEmailResetToken(identityUser, emailResetModel.NewEmail);

            if (String.IsNullOrWhiteSpace(resetToken))
                return BadRequest("Something went wrong preparing the reset!");
            
            IdentityResult emailChangeResult =
                await _identityServiceAccess.ResetEmailWithEmail(identityUser, emailResetModel.NewEmail, resetToken);

            if (!emailChangeResult.Succeeded)
                return BadRequest(emailChangeResult.Errors);
            
            string confirmationToken = await _identityServiceAccess.GenerateEmailConfirmationToken(identityUser);
            await _emailService.EmailConfirmation(identityUser.Email, confirmationToken);

            normalUser.Email = emailResetModel.NewEmail;
            int result = await _userDb.UpdateUser(normalUser);
            
            if (result == 0)
                return BadRequest("Something went wrong while resetting!");

            return Ok("The email has been changed! Check your new email for a confirmation!");
        }

        [HttpPost("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string emailConfirmToken)
        {
            if (String.IsNullOrWhiteSpace(emailConfirmToken))
                return NotFound("No token was supplied!");
            
            string userToken = HttpContext.User.Identity.GetSubjectId();
            
            if (String.IsNullOrWhiteSpace(userToken))
                return BadRequest("No user found in the request.");

            IdentityUser identityUser = await _userDb.GetIdentityUserById(userToken);

            if (identityUser == null)
                return NotFound("Could not find an user with that email!");

            IdentityResult emailConfirmResult = await _identityServiceAccess.ConfirmEmail(identityUser, emailConfirmToken);

            if (!emailConfirmResult.Succeeded)
                return BadRequest(emailConfirmResult);

            return Ok("Your email has been confirmed!");
        }
        

        [HttpPost("resetPassword")]
        public async Task<ActionResult> ChangePasswordWithoutEmail(PasswordResetWithoutEmailModel passwordResetModel)
        {
            if (!passwordResetModel.NewPassword.Equals(passwordResetModel.NewPasswordConfirmation))
                return BadRequest("The passwords are not the same!");

            string userToken = HttpContext.User.Identity.GetSubjectId();
            
            if (String.IsNullOrWhiteSpace(userToken))
                return BadRequest("No user found in the request.");

            IdentityUser identityUser = await _userDb.GetIdentityUserById(userToken);

            if (identityUser == null)
                return NotFound("Could not find an user with that email!");

            if (!identityUser.EmailConfirmed)
                return BadRequest("Your email is not confirmed!");

            string resetToken = await _identityServiceAccess.GeneratePasswordResetToken(identityUser);

            IdentityResult passwordResetResult = await _identityServiceAccess.ResetPassword(identityUser,
                resetToken,
                passwordResetModel.NewPassword);

            if (!passwordResetResult.Succeeded)
                return BadRequest(passwordResetResult.Errors);

            return Ok("Reset the password.");
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUserDetails(string email)
        {
            string userToken = HttpContext.User.Identity.GetSubjectId();
            
            if (String.IsNullOrWhiteSpace(userToken))
                return BadRequest("No user found in the request.");

            IdentityUser identityUser = await _userDb.GetIdentityUserById(userToken);
            
            if (identityUser == null)
                return NotFound("No user found in the request.");

            User normalUser = await _userDb.GetUserByEmail(identityUser.Email);
            
            if (normalUser == null)
                return BadRequest("No user found in the request.");

            normalUser.RegisteredAccount = null;
            normalUser.EmailConfirmed = identityUser.EmailConfirmed;
            
            return Ok(normalUser);
        }

        [AllowAnonymous]
        [HttpPost("resetPasswordWithEmail")]
        public async Task<ActionResult> ResetPasswordViaEmail(PasswordResetModel passwordResetModel)
        {
            if (!passwordResetModel.NewPassword.Equals(passwordResetModel.NewPasswordConfirmation))
                return BadRequest("The passwords are not the same!");

            IdentityUser identityUser = await _userDb.GetIdentityUserByEmail(passwordResetModel.Email);

            if (identityUser == null)
                return BadRequest("Could not find an user with that email!");

            if (!identityUser.EmailConfirmed)
                return BadRequest("Your email is not confirmed!");

            IdentityResult passwordResetResult = await _identityServiceAccess.ResetPasswordWithEmail(identityUser,
                passwordResetModel.ResetToken,
                passwordResetModel.NewPassword);

            if (!passwordResetResult.Succeeded)
                return BadRequest(passwordResetResult.Errors);

            return Ok("Reset the password.");
        }

        [AllowAnonymous]
        [HttpPost("sendPasswordResetEmail")]
        public async Task<ActionResult> SendPasswordResetEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return BadRequest("No email provided!");
            
            await _emailService.RequestPasswordReset(email);

            return Ok("Sent the password reset email.");
        }
    }
}