using Fiveways.Insight.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;
        internal ILogger logger;

        public GenericRepository(DbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
            dbSet = context.Set<TEntity>();
        }
        public async Task<bool> CreateAsync(TEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            try
            {
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                
                logger.Error(e, "");
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> ExecuteQryAsync(string command, object[] parameters = null)
        {
            try
            {
                var reslut = parameters != null
                    ? await context.Database.SqlQuery<TEntity>(command, parameters).ToListAsync()
                    : await context.Database.SqlQuery<TEntity>(command).ToListAsync();
                return reslut;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<TEntity> GetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> SaveAsync(int id, TEntity newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
