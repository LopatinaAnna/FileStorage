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
    /// SharedFileService
    /// </summary>
    public class SharedFileService : ISharedFileService
    {
        private IUnitOfWork Database { get; set; }

        private readonly IMapper mapper;

        public SharedFileService(IUnitOfWork _uow, IMapper _mapper)
        {
            Database = _uow;
            mapper = _mapper;
        }

        public async Task<IEnumerable<SharedFileDto>> GetAll()
        {
            return mapper.Map<IEnumerable<SharedFileDto>>(await Database.SharedFiles.GetAll());
        }

        public async Task<SharedFileDto> GetSharedFile(string sharedLink)
        {
            var item = await Database.SharedFiles.GetWithPredicate(x => x.SharedLink == sharedLink);
            return mapper.Map<SharedFileDto>(item);
        }

        public async Task<SharedFileDto> GetByName(string fileName, string userName)
        {
            var item = await Database.SharedFiles.GetWithPredicate(x => x.UserName == userName && x.Name == fileName);
            return mapper.Map<SharedFileDto>(item);
        }

        public async Task<IEnumerable<SharedFileDto>> GetByAllByUserName(string userName)
        {
            return mapper.Map<IEnumerable<SharedFileDto>>(
                await Database.SharedFiles.Get(x => x.UserName == userName));
        }

        public async Task Create(SharedFileDto item)
        {
            Database.SharedFiles.Create(mapper.Map<SharedFile>(item));
            await Database.SaveAsync();
        }

        public async Task Delete(string userName, string link)
        {
            var item = await Database.SharedFiles.GetWithPredicate(x => x.UserName == userName && x.SharedLink == link);
            Database.SharedFiles.Delete(item.Id);
            await Database.SaveAsync();
        }

        public async Task DeleteAll(string userName)
        {
            var sharedFiles = await Database.SharedFiles.Get(x => x.UserName == userName);
            foreach (var item in sharedFiles)
            {
                Database.SharedFiles.Delete(item.Id);
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