using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.BLL.Interfaces;
using FileStorage.DAL.Entities;
using FileStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Services
{
    /// <summary>
    /// StorageService
    /// </summary>
    public class StorageService : IStorageService
    {
        private IUnitOfWork Database { get; set; }

        private readonly IMapper mapper;

        public StorageService(IUnitOfWork _uow, IMapper _mapper)
        {
            Database = _uow;
            mapper = _mapper;
        }

        public async Task<IEnumerable<StorageDto>> GetAllStorages()
        {
            return mapper.Map<IEnumerable<StorageDto>>(await Database.Storages.GetAll());
        }

        public async Task<int> GetStorageIdByUserName(string userName)
        {
            var storage = await Database.Storages.GetWithPredicate(x => x.UserName == userName);
            return storage is null ? 0 : storage.Id;
        }

        public async Task CreateStorage(string userName)
        {
            if (!await IsStorageExist(userName))
            {
                Database.Storages.Create(new Storage()
                {
                    UserName = userName,
                    StorageSize = 0,
                    CreationDate = DateTime.Now.ToString("s")
                });
                await Database.SaveAsync();
            }
        }

        public async Task<bool> IsStorageExist(string userName)
        {
            return await Database.Storages.GetWithPredicate(x => x.UserName == userName) != null;
        }

        public async Task<long> GetStorageSize(int storageId)
        {
            var storage = await Database.Storages.GetWithPredicate(x => x.Id == storageId);
            return storage.StorageSize;
        }

        public async Task IncreaseStorageSize(int storageId, int length)
        {
            var storage = await Database.Storages.GetWithPredicate(x => x.Id == storageId);
            storage.StorageSize += length;
            await Database.SaveAsync();
        }

        public async Task ReduceStorageSize(int storageId, int length)
        {
            var storage = await Database.Storages.GetWithPredicate(x => x.Id == storageId);
            storage.StorageSize -= length;
            await Database.SaveAsync();
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