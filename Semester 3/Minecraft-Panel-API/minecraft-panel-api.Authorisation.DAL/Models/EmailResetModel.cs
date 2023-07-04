using System.ComponentModel.DataAnnotations;

namespace minecraft_panel_api.Authorisation.DAL.Models
{
    public class EmailResetModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "Email is too long!")]
        [MinLength(0, ErrorMessage = "Email can't be empty!")]
        public string CurrentEmail { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "Email is too long!")]
        [MinLength(0, ErrorMessage = "Email can't be empty!")]
        public string NewEmail { get; set; }
    }
}