using FileStorage.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// ISharedFileService interface
    /// </summary>
    public interface ISharedFileService : IDisposable
    {
        Task<IEnumerable<SharedFileDto>> GetAll();

        Task<SharedFileDto> GetSharedFile(string sharedLink);

        Task<SharedFileDto> GetByName(string fileName, string userName);

        Task<IEnumerable<SharedFileDto>> GetByAllByUserName(string userName);

        Task Create(SharedFileDto item);

        Task Delete(string userName, string link);

        Task DeleteAll(string userName);
    }
}