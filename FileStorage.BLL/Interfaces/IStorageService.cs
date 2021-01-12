using FileStorage.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// IStorageService interface
    /// </summary>
    public interface IStorageService : IDisposable
    {
        Task<IEnumerable<StorageDto>> GetAllStorages();

        Task<int> GetStorageIdByUserName(string userName);

        Task CreateStorage(string userName);

        Task<bool> IsStorageExist(string userName);

        Task<long> GetStorageSize(int storageId);

        Task IncreaseStorageSize(int storageId, int length);

        Task ReduceStorageSize(int storageId, int length);
    }
}