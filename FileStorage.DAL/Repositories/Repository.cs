using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FileStorageContext context;

        private readonly DbSet<T> dbSet;

        public Repository(FileStorageContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }

        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var item = dbSet.Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get(Func<T, bool> predicate)
        {
            var storages = await dbSet.ToListAsync();
            return storages.Where(predicate).ToList();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetWithPredicate(Func<T, bool> predicate)
        {
            var storages = await dbSet.ToListAsync();
            return storages.FirstOrDefault(predicate);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}