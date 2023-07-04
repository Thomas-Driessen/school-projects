using Bunit;
using Kwetter_Front_end_WASM.Pages;
using Kwetter_Front_end_WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kwetter_Front_end_WASM.Test
{
    public class FeedTest : TestContext
    {
        private readonly HttpClient _tweetApiHttpClient;
        public FeedTest(IHttpClientFactory tweetApiHttpClient)
        {
            _tweetApiHttpClient = tweetApiHttpClient.CreateClient("kwetter-tweet-api");
        }

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
            var cut = RenderComponent<Feed>();

            // Act - click button to increment counter
            var result = await _tweetApiHttpClient.GetFromJsonAsync<List<Post>>("/api/v1/post");

            // Assert that the counter was incremented
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }
    }
}
