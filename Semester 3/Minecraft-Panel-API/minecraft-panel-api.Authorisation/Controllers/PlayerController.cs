using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Authorisation.DAL.Context;
using minecraft_panel_api.Authorisation.DAL.Models;

namespace minecraft_panel_api.Authorisation.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("/api/main/[controller]")]
    public class PlayerController : ControllerBase
    {
        private MinecraftPluginContext _context;
        
        public PlayerController(MinecraftPluginContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }
        
        //[HttpPost]
        //public async Task<IActionResult> CreatePlayer(Player player)
        //{
        //    await _context.Players.AddAsync(player);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}