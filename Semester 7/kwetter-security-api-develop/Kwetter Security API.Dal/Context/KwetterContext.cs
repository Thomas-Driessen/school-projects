using Kwetter_Security_API.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Kwetter_Security_API.Dal.Context;

public class KwetterContext : DbContext
{
    public KwetterContext(DbContextOptions options) : base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<UserFollow> Following { get; set; }
}