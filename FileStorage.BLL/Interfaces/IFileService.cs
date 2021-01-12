using FileStorage.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// IFileService interface
    /// </summary>
    public interface IFileService : IDisposable
    {
        Task<IEnumerable<FileDto>> GetAllFiles();

        Task<IEnumerable<FileDto>> GetStorageFiles(int storageId);

        Task<IEnumerable<FileDto>> GetTrashFiles(int storageId);

        Task<IEnumerable<FileDto>> SearchFiles(int storageId, string search);

        Task<FileDto> GetFileByName(string fileName, int storageId);

        Task AddFileToTrash(int storageId, string fileName);

        Task RestoreFileFromTrash(int storageId, string fileName);

        Task RemoveFileFromTrash(int storageId, string fileName);

        Task DeleteAllFromTrash(int storageId);

        Task<bool> IsFileExist(string fileName, int storageId);

        Task AddFile(FileDto item);
    }
}