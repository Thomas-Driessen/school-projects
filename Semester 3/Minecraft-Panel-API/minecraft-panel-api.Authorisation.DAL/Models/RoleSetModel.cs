using Microsoft.AspNetCore.Identity;

namespace minecraft_panel_api.Authorisation.DAL.Models
{
    public class RoleSetModel
    {
        public IdentityUser User { get; set; }
        public IdentityRole Role { get; set; }
    }
}