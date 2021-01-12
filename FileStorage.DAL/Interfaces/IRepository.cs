using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Get(Func<T, bool> predicate);

        Task<T> Get(int id);

        Task<T> GetWithPredicate(Func<T, bool> predicate);

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}