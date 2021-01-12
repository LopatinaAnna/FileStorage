using System.ComponentModel.DataAnnotations;

namespace FileStorage.PL.Models
{
    /// <summary>
    /// ChangePasswordModel
    /// </summary>
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The value {0} must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "New password and its confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }
}