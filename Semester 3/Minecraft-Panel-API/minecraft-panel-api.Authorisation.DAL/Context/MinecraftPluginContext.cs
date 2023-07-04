using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Authorisation.DAL.Models;

namespace minecraft_panel_api.Authorisation.DAL.Context
{
    public class MinecraftPluginContext : IdentityDbContext
    {
        public MinecraftPluginContext(DbContextOptions<MinecraftPluginContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}