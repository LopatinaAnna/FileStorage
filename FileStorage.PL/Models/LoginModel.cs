using System.ComponentModel.DataAnnotations;

namespace FileStorage.PL.Models
{
    /// <summary>
    /// LoginModel
    /// </summary>
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}