using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbEntities;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbEntities = dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbEntities.Add(entity);
        }

        public Task<TEntity> CreateAsync(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(Guid id)
        {
            return await _dbEntities.FindAsync(id);
        }

        public Task<IList<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbEntities.AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}