using Bunit;
using Kwetter_Front_end_WASM.Pages;
using Kwetter_Front_end_WASM.Shared.Components.Post;
using Kwetter_Front_end_WASM.Shared.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RestSharp;

namespace Kwetter_Front_end_WASM.Test
{
    public class FeedTest : TestContext
    {
        [Fact]
        public void ClickingButtonIncrementsCounter()
        {
            // Arrange
            var cut = RenderComponent<Feed>();

            // Act - click button to increment counter
            cut.Find("button").Click();

            // Assert that the counter was incremented
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }

        [Fact]
        public async Task GetFeedConntent()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Feed>();

            var tasks = cut.FindComponents<PostComponent>();

            Assert.Equal(2, tasks.Count);


            //var _httpMessageHandler = new Mock<HttpMessageHandler>();
            //var httpClient = new HttpClient(_httpMessageHandler.Object);
            //var mockFactory = new Mock<IHttpClientFactory>();
            //
            //httpClient.BaseAddress = new Uri("http://localhost:5169"); //New code
            //
            //mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
            //
            //// Act - click button to increment counter
            //var result = await httpClient.GetFromJsonAsync<List<Post>>("/api/v1/post");
            //var tring = "";

            // Assert that the counter was incremented
            //cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
            //Assert.Equal()
        }
    }
}
