using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;
using minecraft_panel_api.Interaction.Hubs;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace minecraft_panel_api.Interaction.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/mc/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IHubContext<SocketTestHub> _hub;
        private readonly IPlayerDbAccess _playerDb;
        private readonly IConfiguration _configuration;
        private readonly IRestClient _restClient;

        public PlayerController(IHubContext<SocketTestHub> hub, IPlayerDbAccess playerDb, IConfiguration configuration, IRestClient restClient)
        {
            _hub = hub;
            _playerDb = playerDb;
            _configuration = configuration;
            _restClient = restClient;
        }

        [HttpPost("getPlayerData")]
        public async Task<ActionResult<Player>> GetPlayerData(Player player)
        {
            return await _playerDb.GetPlayer(player);
        }
        
        [HttpGet]
        public async Task<ActionResult<JArray>> GetPlayersMC()
        {
            _restClient.BaseUrl = new Uri(_configuration["PluginBaseUrl"]);
            
            var request = new RestRequest("/player/players", DataFormat.Json);
            request.AddHeader("content-type", "application/json");

            IRestResponse<JArray> onlinePlayers = await _restClient.ExecuteAsync<JArray>(request, CancellationToken.None);

            if (onlinePlayers.Data == null)
                return NoContent();

            return onlinePlayers.Data;
        }
    }
}