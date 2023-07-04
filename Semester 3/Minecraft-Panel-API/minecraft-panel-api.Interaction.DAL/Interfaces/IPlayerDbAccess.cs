using System.Threading.Tasks;
using minecraft_panel_api.Interaction.DAL.Models;

namespace minecraft_panel_api.Interaction.DAL.Interfaces
{
    public interface IPlayerDbAccess
    {
        public Task<Player> GetPlayer(Player player);
    }
}