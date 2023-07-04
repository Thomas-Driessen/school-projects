using Kwetter_Post_API.DAL.Models;

namespace Kwetter_Post_API.DAL.Interfaces;

public interface IPostAccess
{
    public Task<List<Post>> GetPosts();
    public Task<List<Post>> GetTweetsFromUser(Guid id);
    public Task<Post> CreatePost(Post post);
}