using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Interaction.Controllers;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.DAL.Models;
using minecraft_panel_api.Interaction.Hubs;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using Xunit;

namespace minecraft_panel_api.Interaction.Tests
{
    public class PlayerControllerTest
    {
        //Test if this endpoint returns OK using a mock httpclient
        [Theory]
        [MemberData(nameof(TestGetPlayersViaEndpointDataSet))]
        public async Task TestGetPlayersViaEndpoint(string expectedJson)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {
                    "PluginBaseUrl", ""
                }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var socketHubMock = new Mock<IHubContext<SocketTestHub>>();
            PlayerDbAccessMock playerDbAccess = new PlayerDbAccessMock();
            
            var client = MockRestClient<JArray>(HttpStatusCode.OK, expectedJson);
            
            PlayerController playerController = new PlayerController(socketHubMock.Object, playerDbAccess, configuration, client);

            var result = await playerController.GetPlayersMC();
            
            Assert.Equal(JArray.Parse(expectedJson), result.Value);
        }
        
        public static IEnumerable<object[]> TestGetPlayersViaEndpointDataSet()
        {
            List<object[]> result = new List<object[]>();

            string json = @"
            [ 
                { ""Username"" : ""TestPlayer123"" },
                { ""Username"" : ""SuperCool123"" }
            ]";
            
            result.Add(new object[] { json });

            return result;
        }
        
        // Check if player data returns the expected Player
        [Theory]
        [MemberData(nameof(TestGetUserDataDataSet))]
        public async void TestGetUserData(Player expectedPlayer)
        {
            //Arange
            var socketHubMock = new Mock<IHubContext<SocketTestHub>>();
            PlayerDbAccessMock playerDbAccess = new PlayerDbAccessMock();
            PlayerController playerController = new PlayerController(socketHubMock.Object, playerDbAccess, null, null);
            
            //Act
            ActionResult<Player> result = await playerController.GetPlayerData(expectedPlayer);


            //Assert
            Assert.Equal(expectedPlayer.UserName, result.Value.UserName);
        }

        public static IEnumerable<object[]> TestGetUserDataDataSet()
        {
            List<object[]> result = new List<object[]>();

            Player playerProvided = new Player
            {
                UserName = "Test"
            };
            
            result.Add(new object[] { playerProvided });

            return result;
        }

        public class PlayerDbAccessMock : IPlayerDbAccess
        {
            public async Task<Player> GetPlayer(Player player)
            {
                Player playerReturn = new Player
                {
                    UserName = "Test"
                };

                return playerReturn;
            }
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