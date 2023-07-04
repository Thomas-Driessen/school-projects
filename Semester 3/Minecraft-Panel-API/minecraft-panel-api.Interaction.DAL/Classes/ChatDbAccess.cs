using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Interaction.DAL.Context;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;

namespace minecraft_panel_api.Interaction.DAL.Classes
{
    public class ChatDbAccess : IChatDbAccess
    {
        private readonly MinecraftPluginContext _context;

        public ChatDbAccess(MinecraftPluginContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> GetChatMessagesWithAmount(Pagination pagination)
        {
            return await _context.ChatMessages.OrderByDescending(order => order.TimeStamp).Skip(pagination.Skip).Take(pagination.Take).ToListAsync();
        }

        public async Task<int> GetTotalMessageCount()
        {
            return await _context.ChatMessages.CountAsync();
        }

        public async Task<int> SaveChatMessage(ChatMessage chatMessage)
        {
            await _context.ChatMessages.AddAsync(chatMessage);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}