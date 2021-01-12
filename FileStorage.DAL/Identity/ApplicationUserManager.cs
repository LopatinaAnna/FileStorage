using FileStorage.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace FileStorage.DAL.Identity
{
    /// <summary>
    /// Manage users, add them to the database and authenticate.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}