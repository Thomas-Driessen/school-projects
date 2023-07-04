using System.Net.Http.Json;
using Kwetter_Front_end_WASM.Shared.Interfaces;
using Kwetter_Front_end_WASM.Shared.Models;
using Kwetter_Security_API.Dal.Models;
using Newtonsoft.Json;

namespace Kwetter_Front_end_WASM.Shared.Services;

public class UserService : IUserService
{
    private readonly HttpClient _securityApiHttpClient;
    
    public UserService(IHttpClientFactory securityApiHttpClient)
    {
        _securityApiHttpClient = securityApiHttpClient.CreateClient("kwetter-security-api");
    }
    
    public async Task<User> GetUser(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _securityApiHttpClient.GetAsync($"/api/v1/user?Id={id}");
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
    
    public async Task<User> CreateUser(User user)
    {
        try
        {
            HttpResponseMessage responseMessage;
            
            if (user == null)
            {
                Console.WriteLine("No user supplied");
                User newUser = new User()
                {
                    Username = "",
                    Email = "",
                    Biography = "",
                    CreatedAt = DateTime.UtcNow
                };
                responseMessage = await _securityApiHttpClient.PostAsJsonAsync<User>("/api/v1/user", newUser);
            }
            else
            {
                Console.WriteLine("User supplied");
                responseMessage = await _securityApiHttpClient.PostAsJsonAsync<User>("/api/v1/user", user);
            }
            
            return JsonConvert.DeserializeObject<User>(await responseMessage.Content.ReadAsStringAsync());
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateFollowUser(User userToFollow)
    {
        try
        {
            UserFollow newUserFollow = new UserFollow()
            {
                UserId = new Guid(),
                FollowingUserId = userToFollow.Id,
                FollowDate = DateTime.UtcNow,
                IsFollowing = true
            };

            HttpResponseMessage responseMessage = await _securityApiHttpClient.PostAsJsonAsync<UserFollow>("/api/v1/user/FollowUser", newUserFollow);

            return JsonConvert.DeserializeObject<User>(await responseMessage.Content.ReadAsStringAsync());
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}