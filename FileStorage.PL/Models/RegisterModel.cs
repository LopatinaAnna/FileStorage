using System.ComponentModel.DataAnnotations;

namespace FileStorage.PL.Models
{
    /// <summary>
    /// RegisterModel
    /// </summary>
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The value {0} must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and its confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }
}