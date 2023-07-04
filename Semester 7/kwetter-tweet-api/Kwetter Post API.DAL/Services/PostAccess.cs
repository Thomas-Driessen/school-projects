using Kwetter_Post_API.DAL.Context;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Kwetter_Post_API.DAL.Services;

public class PostAccess : IPostAccess
{
    private readonly KwetterContext _kwetterContext;

    public PostAccess(KwetterContext context)
    {
        _kwetterContext = context;
    }
    
    public async Task<List<Post>> GetPosts()
    {
        return await _kwetterContext.Posts.ToListAsync();
    } 
    
    public async Task<Post> CreatePost(Post post)
    { 
        await _kwetterContext.Posts.AddAsync(post);
        await _kwetterContext.SaveChangesAsync();
        return post;
    }

    public async Task<List<Post>> GetTweetsFromUser(Guid id)
    {
        return await _kwetterContext.Posts.Where(p => p.UserId == id).ToListAsync();
    }
}