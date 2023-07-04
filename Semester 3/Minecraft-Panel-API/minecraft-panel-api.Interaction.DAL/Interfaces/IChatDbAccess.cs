using System.Collections.Generic;
using System.Threading.Tasks;
using minecraft_panel_api.Interaction.DAL.Models;

namespace minecraft_panel_api.Interaction.DAL.Interfaces
{
    public interface IChatDbAccess
    {
        public Task<List<ChatMessage>> GetChatMessagesWithAmount(Pagination pagination);
        public Task<int> GetTotalMessageCount();
        public Task<int> SaveChatMessage(ChatMessage chatMessage);
    }
}