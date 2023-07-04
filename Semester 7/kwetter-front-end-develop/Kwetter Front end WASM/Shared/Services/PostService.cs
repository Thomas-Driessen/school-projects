using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Kwetter_Front_end_WASM.Shared.Interfaces;
using Kwetter_Front_end_WASM.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;

public class PostService : IPostService
{
    private readonly HttpClient _tweetApiHttpClient;
    
    public PostService(IHttpClientFactory tweetApiHttpClient)
    {
        _tweetApiHttpClient = tweetApiHttpClient.CreateClient("kwetter-tweet-api");
    }
    
    public async Task<List<Post>> GetPosts()
    {
        try
        {
            return await _tweetApiHttpClient.GetFromJsonAsync<List<Post>>("/api/v1/post");
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<Post> CreatePost(Post post, IBrowserFile files)
    {
        try
        {
            using MultipartFormDataContent content = new MultipartFormDataContent();
            long maxFileSize = 1024 * 10000;
            StreamContent fileContent = new StreamContent(files.OpenReadStream(maxFileSize));

            //fileContent.Headers.ContentType = new MediaTypeHeaderValue(files.ContentType);

            post.Id = new Guid();
            
            content.Add(content: new StringContent(JsonConvert.SerializeObject(post)), name: "\"post\"");
            content.Add(content: fileContent, name: "\"files\"", fileName: Guid.NewGuid().ToString());
            

            //CreateTweet newTweet = new CreateTweet()
            //{
            //    Post = post,
            //    Files = files
            //};
            //
            //StringContent queryString = new StringContent(JsonConvert.SerializeObject(newTweet));

            HttpResponseMessage httpResponseMessage = await _tweetApiHttpClient.PostAsync("/api/v1/post", content);
            return JsonConvert.DeserializeObject<Post>(await httpResponseMessage.Content.ReadAsStringAsync());
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task<List<Post>> GetTweetsFromUser(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _tweetApiHttpClient.GetAsync($"/api/v1/post/GetTweetsFromUser?Id={id}");
            return JsonConvert.DeserializeObject<List<Post>>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}