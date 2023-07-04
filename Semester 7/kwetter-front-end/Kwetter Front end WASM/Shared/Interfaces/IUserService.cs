using Kwetter_Front_end_WASM.Shared.Models;

namespace Kwetter_Front_end_WASM.Shared.Interfaces;

public interface IUserService
{
    public Task<User?> GetUser(Guid id);
    public Task<User> CreateUser(User user = null);
    public Task<User> UpdateUser(User user);
    public Task<User> DeleteUser(User user);
    public Task<User> CreateFollowUser(User userToFollow);
}