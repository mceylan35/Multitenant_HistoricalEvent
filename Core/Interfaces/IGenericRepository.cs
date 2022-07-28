using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        void Add(T entity);
        IQueryable<T> Queryable();
        Task<T> FindAsync(Guid id);
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(int id);

        Task<IList<T>> GetAllAsync();
        Task<T> CreateAsync(T obj);
          
    }
}
