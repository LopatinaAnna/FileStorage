using Microsoft.AspNet.Identity.EntityFramework;

namespace FileStorage.DAL.Entities
{
    /// <summary>
    /// The user class.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}