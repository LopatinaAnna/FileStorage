using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.DAL.Entities;

namespace FileStorage.BLL.Infrastructure
{
    /// <summary>
    /// Automapper profile
    /// </summary>
    public class AutoMapperProfileBll : Profile
    {
        public AutoMapperProfileBll()
        {
            CreateMap<File, FileDto>().ReverseMap();
            CreateMap<FileDto, File>().ReverseMap();
            CreateMap<SharedFile, SharedFileDto>().ReverseMap();
            CreateMap<SharedFileDto, SharedFile>().ReverseMap();
            CreateMap<Storage, StorageDto>().ReverseMap();
            CreateMap<StorageDto, Storage>().ReverseMap();
        }
    }
}