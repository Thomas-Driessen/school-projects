using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;
using minecraft_panel_api.Interaction.Hubs;

namespace minecraft_panel_api.Interaction.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/mc/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<SocketTestHub> _hub;
        private readonly IChatDbAccess _chatDb;

        public ChatController(IHubContext<SocketTestHub> hub, IChatDbAccess chatDb)
        {
            _hub = hub;
            _chatDb = chatDb;
        }
        
        [HttpPost("getChatMessagesWithAmount")]
        public async Task<ActionResult<List<ChatMessage>>> GetChatMessagesWithAmount(Pagination pagination)
        {
            List<ChatMessage> result = await _chatDb.GetChatMessagesWithAmount(pagination);

            if (result == null)
                return BadRequest("No messages found.");

            return result;
        }

        [HttpGet("getTotalMessageCount")]
        public async Task<int> GetTotalMessageCount()
        {
            int totalRows = await _chatDb.GetTotalMessageCount();

            return totalRows;
        }
        
        [HttpPost("saveChatMessage")]
        public async Task<ActionResult<string>> SaveChatMessage(ChatMessage chatMessage)
        {
            int result = await _chatDb.SaveChatMessage(chatMessage);

            if (result == 0)
                return BadRequest("No rows affected, something might've went wrong.");

            return "Message saved";
        }

        public async Task SendMessagePost(string test)
        {
            await _hub.Clients.All.SendAsync("PlayerQuitEvent", test);
        }
    }
}