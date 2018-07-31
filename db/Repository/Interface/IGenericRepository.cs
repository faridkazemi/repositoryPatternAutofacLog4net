using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");

        Task<TEntity> GetAsync(int Id);

        Task<bool> CreateAsync(TEntity newEntity);

        Task<bool> SaveAsync(int id, TEntity newEntity);

        Task<bool> DeleteAsync(int id);
    }
}
