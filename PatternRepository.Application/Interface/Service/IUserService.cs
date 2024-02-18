using PatternRepository.Application.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Service
{
    public interface IUserService<T> where T : ApplicationUser
    {
        Task<T> FirstOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(string id, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, bool asTracking = false, params Expression<Func<T, object>>[] includes);
        Task<string> CreateAsync(T model);
        Task<string> UpdateAsync(T model);
        Task<string> DeleteAsync(T model);
    }
}
