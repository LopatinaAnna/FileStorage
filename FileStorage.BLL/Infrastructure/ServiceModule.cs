using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Repositories;
using Ninject.Modules;

namespace FileStorage.BLL.Infrastructure
{
    /// <summary>
    /// Service module, binding UnitOfWork
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}