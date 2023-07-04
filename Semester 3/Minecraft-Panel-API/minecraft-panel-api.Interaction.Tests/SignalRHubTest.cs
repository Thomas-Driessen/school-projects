using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using minecraft_panel_api.Interaction.Hubs;
using Moq;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using minecraft_panel_api.Interaction.Controllers;
using Xunit;

namespace minecraft_panel_api.Interaction.Tests
{
    public class SignalRHubTest
    {
        // SignalR experiment
        [Fact]
        public async Task SignalR_OnConnect_ShouldReturn3Messages()
        {
            Mock<IHubClients> mockClients = new Mock<IHubClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            var hubContext = new Mock<IHubContext<SocketTestHub>>();
            hubContext.Setup(x => x.Clients).Returns(() => mockClients.Object);

            ChatController hub = new ChatController(hubContext.Object, null);

            await hub.SendMessagePost("This is a unit test.");

            mockClients.Verify(clients => clients.All, Times.Once);
        }
    }
}