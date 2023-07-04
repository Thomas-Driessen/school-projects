using Kwetter_Front_end_WASM.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Kwetter_Front_end_WASM.Shared.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetPosts();
    Task<List<Post>> GetTweetsFromUser(Guid id);
    Task<Post> CreatePost(Post post, IBrowserFile files);
}