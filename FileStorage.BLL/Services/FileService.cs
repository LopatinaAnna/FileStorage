using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.BLL.Infrastructure;
using FileStorage.BLL.Interfaces;
using FileStorage.DAL.Entities;
using FileStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Services
{
    /// <summary>
    /// FileService
    /// </summary>
    public class FileService : IFileService
    {
        private IUnitOfWork Database { get; set; }

        private readonly IMapper mapper;

        public FileService(IUnitOfWork _uow, IMapper _mapper)
        {
            Database = _uow;
            mapper = _mapper;
        }

        public async Task<IEnumerable<FileDto>> GetAllFiles()
        {
            return mapper.Map<IEnumerable<FileDto>>(await Database.Files.GetAll());
        }

        public async Task<IEnumerable<FileDto>> GetStorageFiles(int storageId)
        {
            return mapper.Map<IEnumerable<FileDto>>(
                await Database.Files.Get(x => x.StorageId == storageId && !x.IsRemove));
        }

        public async Task<IEnumerable<FileDto>> GetTrashFiles(int storageId)
        {
            return mapper.Map<IEnumerable<FileDto>>(
                await Database.Files.Get(x => x.StorageId == storageId && x.IsRemove));
        }

        public async Task<IEnumerable<FileDto>> SearchFiles(int storageId, string search)
        {
            return mapper.Map<IEnumerable<FileDto>>(
                await Database.Files.Get(x => x.StorageId == storageId && !x.IsRemove && x.Name.ToLower().Contains(search.ToLower())));
        }

        public async Task<FileDto> GetFileByName(string fileName, int storageId)
        {
            var item = await Database.Files.GetWithPredicate(x => x.StorageId == storageId && x.Name == fileName);
            if (item == null)
                throw new ValidationException("file don't exist");
            return mapper.Map<FileDto>(item);
        }

        public async Task AddFileToTrash(int storageId, string fileName)
        {
            var file = await Database.Files.GetWithPredicate(x => x.StorageId == storageId && x.Name == fileName);
            file.IsRemove = true;
            await Database.SaveAsync();
        }

        public async Task RestoreFileFromTrash(int storageId, string fileName)
        {
            var file = await Database.Files.GetWithPredicate(x => x.StorageId == storageId && x.Name == fileName);
            file.IsRemove = false;
            await Database.SaveAsync();
        }

        public async Task RemoveFileFromTrash(int storageId, string fileName)
        {
            var file = await Database.Files.GetWithPredicate(x => x.StorageId == storageId && x.Name == fileName);
            Database.Files.Delete(file.Id);
            await Database.SaveAsync();
        }

        public async Task DeleteAllFromTrash(int storageId)
        {
            var trashFiles = await Database.Files.Get(x => x.StorageId == storageId && x.IsRemove);
            foreach (var item in trashFiles)
            {
                Database.Files.Delete(item.Id);
                await Database.SaveAsync();
            }
        }

        public async Task<bool> IsFileExist(string fileName, int storageId)
        {
            return await Database.Files.GetWithPredicate(x => x.StorageId == storageId && x.Name == fileName) != null;
        }

        public async Task AddFile(FileDto item)
        {
            if (!await IsFileExist(item.Name, item.StorageId))
            {
                item.CreationDate = DateTime.Now.ToString("s"); // 2008-06-15T21:15:07
                item.SharedLink = Guid.NewGuid().ToString().Substring(2, 10);

                Database.Files.Create(mapper.Map<File>(item));
                await Database.SaveAsync();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
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