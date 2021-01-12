using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.PL.Models;

namespace FileStorage.PL.Infrastructure
{
    /// <summary>
    /// Automapper profile
    /// </summary>
    public class AutoMapperProfilePl : Profile
    {
        public AutoMapperProfilePl()
        {
            CreateMap<FileDto, FileViewModel>().ReverseMap();
            CreateMap<FileViewModel, FileDto>().ReverseMap();
            CreateMap<StorageDto, StorageModel>().ReverseMap();
            CreateMap<StorageModel, StorageDto>().ReverseMap();
            CreateMap<SharedFileDto, SharedViewModel>().ReverseMap();
            CreateMap<SharedViewModel, SharedFileDto>().ReverseMap();
        }
    }
}