using PatternRepository.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> FindAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T?> FisrtOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> FisrtOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<Guid> CreateAsync(T model);
        Task<Guid> UpdateAsync(T model);
        Task<Guid> DeleteAsync(Guid id);
        Task SaveChangesAsync();

    }
}
