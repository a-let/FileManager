using FileManager.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        [Obsolete("Use IQueryById interface")]
        Task<T> GetByIdAsync(int id);
        [Obsolete("Use IQueryForList interface")]
        IEnumerable<T> Get();
        [Obsolete("Use IQueryByName interface")]
        T GetByName(string name);
        Task<T> FindAsync(int id);
        Task SaveAsync(T target);
    }
}
