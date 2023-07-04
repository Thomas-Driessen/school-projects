using System.ComponentModel.DataAnnotations;

namespace minecraft_panel_api.Authorisation.DAL.Models
{
    public class PasswordResetWithoutEmailModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Password is too long!")]
        [MinLength(0, ErrorMessage = "Password can't be empty!")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password is too long!")]
        [MinLength(0, ErrorMessage = "Password can't be empty!")]
        public string NewPasswordConfirmation { get; set; }
    }
}