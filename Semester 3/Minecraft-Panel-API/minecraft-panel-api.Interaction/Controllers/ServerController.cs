using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Server.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace minecraft_panel_api.Interaction.Controllers
{
    [ApiController]
    [Route ("/api/mc/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient _restClient;

        public ServerController(IConfiguration configuration, IRestClient restClient)
        {
            _configuration = configuration;
            _restClient = restClient;
        }
        
        [Authorize]
        [HttpGet("getPlugins")]
        public async Task<ActionResult<JArray>> GetPlugins()
        {
            _restClient.BaseUrl = new Uri(_configuration["PluginBaseUrl"]);

            RestRequest request = new RestRequest("plugin/getplugins", DataFormat.Json);
            request.AddHeader("content-type", "application/json");

            IRestResponse<JArray> pluginListResponse = await _restClient.ExecuteAsync<JArray>(request, CancellationToken.None);
            
            if (pluginListResponse.Data == null)
                return NoContent();
            
            //List<Plugin> pluginList = JsonConvert.DeserializeObject<List<Plugin>>(pluginListResponse.Content);
            
            //var response = new Dictionary<string, List<Plugin>>();

            //response.Add("Plugins", pluginList);

            return pluginListResponse.Data;
        }

        [Authorize]
        [HttpPost("disablePlugin")]
        public async Task<ActionResult<string>> DisablePlugin(Plugin plugin)
        {
            _restClient.BaseUrl = new Uri(_configuration["PluginBaseUrl"]);
            
            RestRequest disablePluginRequest = new RestRequest("plugin/disableplugin", Method.DELETE, DataFormat.Json);

            disablePluginRequest.AddHeader("Accept", "application/json");
            
            string json = JsonConvert.SerializeObject(plugin, Formatting.Indented);

            disablePluginRequest.AddJsonBody(json);
    
            //Change to proper type
            IRestResponse deleteRequestResult = await _restClient.ExecuteAsync<RestResponse>(disablePluginRequest, CancellationToken.None);

            if (deleteRequestResult.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(deleteRequestResult.Content);
            
            if (deleteRequestResult.StatusCode == HttpStatusCode.NotFound)
                return NotFound(deleteRequestResult.Content);
                
            return deleteRequestResult.Content;
        }
    }
}