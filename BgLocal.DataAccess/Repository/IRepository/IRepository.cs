using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BgLocal.DataAccess.Repository.IRepository {
    public interface IRepository<T> where T : class {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> RemoveAsync(int id);
        Task SaveChanges();
    }
}
