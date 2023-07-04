using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kwetter_Security_API.Core.Interfaces;
using Kwetter_Security_API.Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter_Security_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<User> Get(Guid id)
        {
            return await _userService.GetUser(id);
        }

        [HttpPost]
        public async Task<User> Post(User user)
        {
            if (HttpContext.User.Identity != null && !HttpContext.User.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException();

            Guid userId = new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (await _userService.GetUser(userId) != null)
                throw new Exception("User already exists.");
            
            User createdUser;

            if (user.Id == Guid.Empty)
                createdUser = await _userService.CreateUser(HttpContext.User);
            else
                createdUser = await _userService.CreateUser(user);
            
            return createdUser;
        }

        [HttpPost("FollowUser")]
        public async Task<UserFollow> CreateFollowUser(UserFollow userFollow)
        {
            return await _userService.CreateFollowUser(HttpContext.User, userFollow); ;
        }
    }
}