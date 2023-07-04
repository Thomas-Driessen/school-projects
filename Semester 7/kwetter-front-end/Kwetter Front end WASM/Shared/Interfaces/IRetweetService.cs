using Kwetter_Front_end_WASM.Shared.Models;
using Kwetter_Post_API.DAL.Models;

namespace Kwetter_Front_end_WASM.Shared.Interfaces;

public interface IRetweetService
{
    public Task<Retweet> GetRetweet(Guid id);
    public Task<Retweet> CreateRetweet(Post post);
    public Task<Retweet> UpdateRetweet(Retweet retweet);
    public Task<Retweet> DeleteRetweet(Retweet retweet);
}