using FileStorage.DAL.Entities;
using FileStorage.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    /// <summary>
    /// UnitOfWork interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<File> Files { get; }

        IRepository<SharedFile> SharedFiles { get; }

        IRepository<Storage> Storages { get; }

        IRepository<ClientProfile> Clients { get; }

        ApplicationUserManager UserManager { get; }

        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}