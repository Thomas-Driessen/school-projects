using System;
using Microsoft.AspNetCore.Identity;

namespace minecraft_panel_api.Authorisation.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public IdentityUser RegisteredAccount { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool EmailConfirmed { get; set; }
        public Player Player { get; set; }
    }
}