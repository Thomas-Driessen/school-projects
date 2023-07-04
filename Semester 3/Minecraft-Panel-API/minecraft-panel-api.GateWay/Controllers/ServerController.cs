using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using minecraft_panel_api.GateWay.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace minecraft_panel_api.GateWay.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGeneralServerInfo()
        {
            RestClient baseUrl = new RestClient("");
            baseUrl.UseNewtonsoftJson();
            RestRequest serverPlugins = new RestRequest("gateway/mc/server/server/getPlugins", Method.GET, DataFormat.Json);
            RestRequest onlineUsers = new RestRequest("gateway/mc/player/player", Method.GET, DataFormat.Json);

            //serverPlugins.AddHeader("Accept", "application/json");
            //onlineUsers.AddHeader("Accept", "application/json");
            serverPlugins.AddHeader("content-type", "application/json");
            onlineUsers.AddHeader("content-type", "application/json");

            IRestResponse serverData = await baseUrl.ExecuteAsync<RestResponse>(serverPlugins, CancellationToken.None);
            IRestResponse onlinePlayerData = await baseUrl.ExecuteAsync<RestResponse>(onlineUsers, CancellationToken.None);

            dynamic serverInfo = JsonConvert.DeserializeObject<dynamic>(serverData.Content);
            dynamic userInfo = JsonConvert.DeserializeObject<dynamic>(onlinePlayerData.Content);
            
            JObject jsonObject = new JObject();
            jsonObject.Add("Server", serverInfo);
            jsonObject.Add("Players", userInfo);
            
            return Ok(jsonObject);
        }
    }
}