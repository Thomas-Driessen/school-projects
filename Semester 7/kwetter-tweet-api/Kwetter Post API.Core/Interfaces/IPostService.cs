using System.Security.Claims;
using Kwetter_Post_API.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Kwetter_Post_API.Core.Interfaces;

public interface IPostService
{
    public Task<List<Post>> GetPosts();
    public Task<List<Post>> GetTweetsFromUser(Guid id);
    public Task<Post> CreatePost(ClaimsPrincipal user, Post post, IFormFile media);
}