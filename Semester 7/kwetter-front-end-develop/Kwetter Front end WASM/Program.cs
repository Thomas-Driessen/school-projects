using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kwetter_Front_end_WASM;
using Kwetter_Front_end_WASM.Shared.Interfaces;
using Kwetter_Front_end_WASM.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRetweetService, RetweetService>();
builder.Services.AddTransient<KwetterAuthorizationMessageHandler>();

// AddHttpClient is an extension in Microsoft.Extensions.Http

builder.Services.AddHttpClient("kwetter-security-api", client => 
        client.BaseAddress = new Uri(builder.Configuration["Kwetter_API:Security_API"] ?? string.Empty))
    .AddHttpMessageHandler<KwetterAuthorizationMessageHandler>();

builder.Services.AddHttpClient("kwetter-tweet-api", client => 
        client.BaseAddress = new Uri(builder.Configuration["Kwetter_API:Tweet_API"] ?? string.Empty))
    .AddHttpMessageHandler<KwetterAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("kwetter-security-api"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("kwetter-tweet-api"));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
    //options.AuthenticationPaths.RemoteRegisterPath = "http://localhost:8106/auth/realms/Kwetter/login-actions/registration?client_id=kwetter-front-end";
});

await builder.Build().RunAsync();