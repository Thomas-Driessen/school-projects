using Kwetter_Post_API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Kwetter_Post_API.DAL.Context;

public class KwetterContext : DbContext
{
    public KwetterContext(DbContextOptions options) : base(options) {}
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Retweet> Retweets { get; set; }
}