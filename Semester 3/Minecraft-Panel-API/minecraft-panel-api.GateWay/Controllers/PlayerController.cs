using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetPlayerWithUser(InputModel inputModel)
        {
            RestClient baseUrl = new RestClient("");
            baseUrl.UseNewtonsoftJson();
            RestRequest playerDataClientRequest = new RestRequest("gateway/main/user/getUserData", Method.POST, DataFormat.Json);
            RestRequest userDataClientRequest = new RestRequest("gateway/mc/player/getPlayerData", Method.POST, DataFormat.Json);

            userDataClientRequest.AddHeader("Accept", "application/json");
            playerDataClientRequest.AddHeader("Accept", "application/json");

            userDataClientRequest.AddJsonBody(inputModel);
            playerDataClientRequest.AddJsonBody(inputModel);
    
            IRestResponse playerData = await baseUrl.ExecuteAsync<RestResponse>(playerDataClientRequest, CancellationToken.None);
            IRestResponse userData = await baseUrl.ExecuteAsync<RestResponse>(userDataClientRequest, CancellationToken.None);

            dynamic player = JsonConvert.DeserializeObject<dynamic>(playerData.Content);
            dynamic user = JsonConvert.DeserializeObject<dynamic>(userData.Content);
            
            JObject jsonObject = new JObject();
            jsonObject.Add("Player", player);
            jsonObject.Add("User", user);
            
            return Ok(jsonObject);
        }
    }
}