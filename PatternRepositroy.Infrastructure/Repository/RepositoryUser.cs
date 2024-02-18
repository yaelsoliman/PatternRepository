using Microsoft.EntityFrameworkCore;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Interface.Repository;
using PatternRepositroy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepositroy.Infrastructure.Repository
{
    public class RepositoryUser<T>:IRepositoryUser<T> where T : ApplicationUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _set;

        public RepositoryUser(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }

        public async Task<T?> FindAsync(string id, params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            if (includes?.Length > 0)
            {
                var set = _set.AsQueryable();
                foreach (var include in includes)
                    set = set.Include(include);

                entity = await set.FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                entity = await _set.FindAsync(id);
            }
            return entity;
        }
        public async Task<T?> FisrtOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entity = await set.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<T?> FisrtOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entity = await set.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<IEnumerable<T>?> GetListAsync(params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? entities;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entities = await set.ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? entities;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entities = await set.Where(expression).ToListAsync();
            return entities;
        }
        public async Task<string> CreateAsync(T model)
        {

            var x = await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return x.Entity.Id;
        }
        public async Task<string> UpdateAsync(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        //public async Task<string> DeleteAsync(string id)
        //{
        //    T? entity = await FindAsync(id);
        //    if (entity is not null)
        //    {
                
        //        _dbContext.Entry(entity).State = EntityState.Modified;
        //        await _dbContext.SaveChangesAsync();
        //        return id;
        //    }
        //    return string.Empty;
        //}
        public async Task<string> DeleteAsync(T model)
        {
            _set.Remove(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
