using Microsoft.AspNetCore.Identity;

namespace minecraft_panel_api.Authorisation.DAL.Models
{
    public class RegisterModel
    {
        public IdentityUser UserDetails { get; set; }
        public string Password { get; set; }
    }
}