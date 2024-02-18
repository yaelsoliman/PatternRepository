using PatternRepository.Application.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Repository
{
    public interface IRepositoryUser<T> where T : ApplicationUser
    {
        Task<T?> FindAsync(string id, params Expression<Func<T, object>>[] includes);
        Task<T?> FisrtOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> FisrtOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<string> CreateAsync(T model);
        Task<string> UpdateAsync(T model);
        Task<string> DeleteAsync( T model);
        Task SaveChangesAsync();
    }
}
