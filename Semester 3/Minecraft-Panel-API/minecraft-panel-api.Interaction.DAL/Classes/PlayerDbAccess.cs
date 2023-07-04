using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Interaction.DAL.Context;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;

namespace minecraft_panel_api.Interaction.DAL.Classes
{
    public class PlayerDbAccess : IPlayerDbAccess
    {
        private readonly MinecraftPluginContext _context;

        public PlayerDbAccess(MinecraftPluginContext context)
        {
            _context = context;
        }
        
        public async Task<Player> GetPlayer(Player player)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.UserName == player.UserName);
        }
    }
}