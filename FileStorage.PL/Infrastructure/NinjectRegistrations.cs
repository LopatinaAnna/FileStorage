using AutoMapper;
using FileStorage.BLL.Infrastructure;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Services;
using Ninject.Modules;

namespace FileStorage.PL.Infrastructure
{
    /// <summary>
    /// NinjectRegistrations, binding services and automapper profiles
    /// </summary>
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileService>().To<FileService>();
            Bind<ISharedFileService>().To<SharedFileService>();
            Bind<IStorageService>().To<StorageService>();
            Bind<IUserService>().To<UserService>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfileBll>();
                cfg.AddProfile<AutoMapperProfilePl>();
            });
            Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
        }
    }
}