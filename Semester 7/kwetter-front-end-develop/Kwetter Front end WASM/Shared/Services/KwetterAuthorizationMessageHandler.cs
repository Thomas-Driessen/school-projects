using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Kwetter_Front_end_WASM.Shared.Services;

public class KwetterAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public KwetterAuthorizationMessageHandler(IAccessTokenProvider provider, 
        NavigationManager navigation)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "http://localhost:5169", "http://localhost:5298" },
            scopes: new[] { "kwetter.read", "kwetter.write" });
    }
}