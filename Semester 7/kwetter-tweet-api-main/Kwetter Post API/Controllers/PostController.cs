using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter_Post_API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Post>> GetFeed()
    {
        return await _postService.GetPosts();
    }

    [HttpGet("GetTweetsFromUser")]
    public async Task<IEnumerable<Post>> GetTweetsFromUser(Guid id)
    {
        return await _postService.GetTweetsFromUser(id);
    }

    [HttpPost]
    public async Task<Post> Post(Post post, [FromForm] IFormFile files)
    {
        return await _postService.CreatePost(HttpContext.User, post, files);
    }
}