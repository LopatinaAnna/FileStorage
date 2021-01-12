using FileStorage.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FileStorage.DAL.EF
{
    /// <summary>
    /// Data context
    /// </summary>
    public class FileStorageContext : IdentityDbContext<ApplicationUser>
    {
        public FileStorageContext() : base("DefaultConnection")
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<SharedFile> SharedFiles { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}