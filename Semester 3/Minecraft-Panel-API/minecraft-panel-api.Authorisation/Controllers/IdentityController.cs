using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using minecraft_panel_api.Authorisation.DAL.Classes;
using minecraft_panel_api.Authorisation.DAL.Interfaces;
using minecraft_panel_api.Authorisation.DAL.Models;
using Newtonsoft.Json.Linq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace minecraft_panel_api.Authorisation.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly IUserDbAccess _userDb;
        private readonly ITokenServiceAccess _tokenServiceAccess;
        private readonly IIdentityServiceAccess _identityServiceAccess;

        public IdentityController(
            UserManager<IdentityUser> userManager,
            //SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interaction,
            IEventService events,
            IUserDbAccess userDb,
            ITokenServiceAccess tokenServiceAccess,
            IIdentityServiceAccess identityServiceAccess
        )
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _interaction = interaction;
            _events = events;
            _userDb = userDb;
            _tokenServiceAccess = tokenServiceAccess;
            _identityServiceAccess = identityServiceAccess;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel loginModel)
        {
            IdentityUser foundIdentityUser = await _userDb.GetIdentityUserByEmail(loginModel.Email);

            if (foundIdentityUser == null)
                return NotFound("Couldn't find that user.");
            
            SignInResult signInResult =
                await _identityServiceAccess.SignInUserWithPassword(foundIdentityUser.UserName, loginModel.Password, false,
                    false);

            if (!signInResult.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(foundIdentityUser.UserName, "invalid credentials", clientId: "clientpasswordtest"));
                return Unauthorized("Wrong credentials.");
            }

            IList<string> userRoles = await _userDb.GetUserRoles(foundIdentityUser);
                
            await _events.RaiseAsync(new UserLoginSuccessEvent(foundIdentityUser.UserName, foundIdentityUser.Id, foundIdentityUser.UserName, clientId: "clientpasswordtest"));

            var token = await _tokenServiceAccess.IssueJwtToken(foundIdentityUser.Id, foundIdentityUser.UserName, userRoles.First());
            
            var response = new Dictionary<string, string>();

            response.Add("Token", token);
            
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                //await _signInManager.SignOutAsync();
                await _interaction.RevokeTokensForCurrentSessionAsync();
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.Claims.Select(x => x.Subject).ToString(), User.GetDisplayName()));
                return Ok("Logged out");
            }

            return BadRequest("Not authenticated");
        }
    }
}