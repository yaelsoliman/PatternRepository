using JobFinder.Application.Exceptions;
using PatternRepository.Application.Interface.Repository;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepositroy.Infrastructure.Service
{
    public class BaseService<T> : IBaseService<T>
        where T : BaseEntity
    {
        private readonly IRepository<T> _repo;

        public BaseService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual async Task<T> FirstOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            T? result = await _repo.FisrtOrDefaultAsync(includes);
            _ = result ?? throw new NotFoundException(string.Format("{0} Not Found.", typeof(T).Name));
            return result;
        }

        public virtual async Task<T> GetAsync(Guid id, bool asTracking = false, params Expression<Func<T, object>>[] includes)
        {
            T? result = await _repo.FindAsync(id);
            _ = result ?? throw new NotFoundException(string.Format("{0} Not Found.", typeof(T).Name));
            return result;
        }

        public virtual async Task<T> GetAsync(bool asTracking = false, params Expression<Func<T, object>>[] includes)
        {
            T? result = await _repo.FisrtOrDefaultAsync(includes);
            _ = result ?? throw new NotFoundException(string.Format("{0} Not Found.", typeof(T).Name));
            return result;
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes)
        {
            T? result = await _repo.FisrtOrDefaultAsync(expression, includes);
            _ = result ?? throw new NotFoundException(string.Format("{0} Not Found.", typeof(T).Name));
            return result;
        }
        public virtual async Task<IEnumerable<T>> GetListAsync(params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? results=await _repo.GetListAsync(includes);
            _ = results ?? throw new NotFoundException(string.Format("{0} List Not Found.", typeof(T).Name));
            return results;
        }
        public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? results=await _repo.GetListAsync(expression,includes);
            _ = results ?? throw new NotFoundException(string.Format("{0} List Not Found.", typeof(T).Name));
            return results;
        }
        public virtual async Task<Guid> CreateAsync(T model)
        {
            var result= await _repo.CreateAsync(model);
            return result;
        }

        public virtual async Task<Guid> DeleteAsync(Guid id)
        {
            var result = await _repo.DeleteAsync(id);
            return result;
        }

        public virtual async Task<Guid> UpdateAsync(T model)
        {
            var oldModel = await GetAsync(model.Id);
            var baseEntityProperties = new BaseEntity().GetType().GetProperties().Where(x => x.Name != nameof(BaseEntity.IsActive));
            foreach (var property in model.GetType().GetProperties().ToList().Except(baseEntityProperties))
            {
                oldModel.GetType().GetProperty(property.Name)?.SetValue(oldModel, property.GetValue(model));
            }
            var result = await _repo.UpdateAsync(model);
            return result;
        }

       
    }
}
