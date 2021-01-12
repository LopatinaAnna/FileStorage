using FileStorage.DAL.EF;
using FileStorage.DAL.Entities;
using FileStorage.DAL.Identity;
using FileStorage.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories
{
    /// <summary>
    /// UnitOfWork, encapsulates all managers for working with entities and stores the general data context
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileStorageContext context;

        private Repository<File> fileRepository;

        private Repository<SharedFile> sharedFileRepository;

        private Repository<Storage> storageRepository;

        private Repository<ClientProfile> clientsRepository;

        public UnitOfWork()
        {
            context = new FileStorageContext();
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
        }

        public IRepository<File> Files
        {
            get
            {
                if (fileRepository == null)
                    fileRepository = new Repository<File>(context);
                return fileRepository;
            }
        }

        public IRepository<SharedFile> SharedFiles
        {
            get
            {
                if (sharedFileRepository == null)
                    sharedFileRepository = new Repository<SharedFile>(context);
                return sharedFileRepository;
            }
        }

        public IRepository<Storage> Storages
        {
            get
            {
                if (storageRepository == null)
                    storageRepository = new Repository<Storage>(context);
                return storageRepository;
            }
        }

        public IRepository<ClientProfile> Clients
        {
            get
            {
                if (clientsRepository == null)
                    clientsRepository = new Repository<ClientProfile>(context);
                return clientsRepository;
            }
        }

        public ApplicationUserManager UserManager { get; }

        public ApplicationRoleManager RoleManager { get; }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}