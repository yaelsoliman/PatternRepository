using PatternRepository.Domain.Common;
using PatternRepository.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Service
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> FirstOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Guid id, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<Guid> CreateAsync(T model);
        Task<Guid> UpdateAsync(T model);
        Task<Guid> DeleteAsync(Guid id);
      
    }
}
