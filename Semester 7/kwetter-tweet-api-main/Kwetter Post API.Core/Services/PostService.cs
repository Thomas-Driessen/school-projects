using System.Security.Claims;
using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace Kwetter_Post_API.Core.Services;

public class PostService : IPostService
{
    private readonly IPostAccess _postAccess;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IGoogleCloudStorageService _googleCloudStorageService;

    public PostService(IPostAccess postAccess, IPublishEndpoint publishEndpoint, IGoogleCloudStorageService googleCloudStorageService)
    {
        _postAccess = postAccess;
        _publishEndpoint = publishEndpoint;
        _googleCloudStorageService = googleCloudStorageService;
    }
    
    public async Task<List<Post>> GetPosts()
    {
        return await _postAccess.GetPosts();
    } 
    
    public async Task<Post> CreatePost(ClaimsPrincipal user, Post post, IFormFile media)
    {
        post.CreatedDate = DateTime.UtcNow;
        post.UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        Uri test = new Uri(await _googleCloudStorageService.UploadFileAsync(media, Guid.NewGuid() + ".jpg"));
        post.MediaImage = test;
        await _postAccess.CreatePost(post);
        return post;
    }

    public async Task<List<Post>> GetTweetsFromUser(Guid id)
    {
        return await _postAccess.GetTweetsFromUser(id);
    }
}