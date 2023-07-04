using Microsoft.EntityFrameworkCore;
using minecraft_panel_api.Interaction.DAL.Models;

namespace minecraft_panel_api.Interaction.DAL.Context
{
    public class MinecraftPluginContext : DbContext
    {
        public MinecraftPluginContext(DbContextOptions<MinecraftPluginContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}