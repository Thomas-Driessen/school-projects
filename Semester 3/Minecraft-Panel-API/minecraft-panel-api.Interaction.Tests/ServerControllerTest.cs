using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Interaction.Controllers;
using minecraft_panel_api.Interaction.Hubs;
using minecraft_panel_api.Server.Models;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace minecraft_panel_api.Interaction.Tests
{
    public class ServerControllerTest
    {
        //Test if this endpoint returns OK using a mock httpclient
        [Theory]
        [MemberData(nameof(TestGetPluginsDataSet))]
        public async Task TestGetPlugins(string expectedJson)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {
                    "PluginBaseUrl", ""
                }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var client = MockRestClient<JArray>(HttpStatusCode.OK, expectedJson);
            
            ServerController serverController = new ServerController(configuration, client);

            var result = await serverController.GetPlugins();
            
            Assert.Equal(JArray.Parse(expectedJson), result.Value);
        }
        
        public static IEnumerable<object[]> TestGetPluginsDataSet()
        {
            List<object[]> result = new List<object[]>();

            string json = @"
            [ 
                { 
                    ""PluginName"" : ""EnabledTestPlugin"",
                    ""isEnabled"" : true
                },
                { 
                    ""PluginName"" : ""DisabledTestPlugin"",
                    ""isEnabled"" : false
                }
            ]";
            
            result.Add(new object[] { json });

            return result;
        }
        
        //Test if this endpoint returns OK using a mock httpclient
        //[Theory]
        [MemberData(nameof(TestDisablePluginDataSet))]
        public async Task TestDisablePlugin(Plugin plugin, string expectedJson, string expectedString)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {
                    "PluginBaseUrl", ""
                }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var client = MockRestClient<RestResponse>(HttpStatusCode.OK, expectedJson);
            
            ServerController serverController = new ServerController(configuration, client);

            var result = await serverController.DisablePlugin(plugin);
            
            Assert.Equal(expectedString, result.Value);
        }
        
        public static IEnumerable<object[]> TestDisablePluginDataSet()
        {
            List<object[]> result = new List<object[]>();

            Plugin plugin = new Plugin
            {
                PluginName = "Test plugin",
                IsEnabled = true
            };
            
            string json = @"
                { 
                    ""ResponseText"" : ""It worked!"",
                    ""StatusCode"" : 200
                }";

            string expectedString = "It worked!";
            
            result.Add(new object[] { plugin, json, expectedString });

            return result;
        }
        
        public static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string json) 
            where T : new()
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            var response =  new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(data);

            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient
                .Setup(x => x.ExecuteAsync<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response.Object);
            return mockIRestClient.Object;
        }
    }
}