using System.Net.Http.Json;
using System.Security.Claims;
using Kwetter_Front_end_WASM.Shared.Interfaces;
using Kwetter_Front_end_WASM.Shared.Models;
using Kwetter_Post_API.DAL.Models;
using Kwetter_Security_API.Dal.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Kwetter_Front_end_WASM.Shared.Services;

public class RetweetService : IRetweetService
{
    private readonly HttpClient _securityApiHttpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public RetweetService(IHttpClientFactory securityApiHttpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _securityApiHttpClient = securityApiHttpClient.CreateClient("kwetter-security-api");
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Retweet> CreateRetweet(Post post)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        Retweet newUserFollow = new Retweet()
        {
            Id = Guid.NewGuid(),
            UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
            Post = post,
            RetweetedAt = DateTime.UtcNow,
            DeletedRetweetAt = null,
            IsDeleted = false
        };

        HttpResponseMessage responseMessage = await _securityApiHttpClient.PostAsJsonAsync<Retweet>("/api/v1/user/FollowUser", newUserFollow);

        return JsonConvert.DeserializeObject<Retweet>(await responseMessage.Content.ReadAsStringAsync());
    }

    public Task<Retweet> DeleteRetweet(Retweet retweet)
    {
        throw new NotImplementedException();
    }

    public Task<Retweet> GetRetweet(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Retweet> UpdateRetweet(Retweet retweet)
    {
        throw new NotImplementedException();
    }
}