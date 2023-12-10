using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected ApplicationContext dbContext;

        public GenericRepository(ApplicationContext dbContext)
            => this.dbContext = dbContext;

        public IEnumerable<TEntity> FindAll() =>
            dbContext.Set<TEntity>()
                    .ToList();

        public IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition) =>
            dbContext.Set<TEntity>()
                .Where(condition)
                .ToList();

        public void Create(TEntity entity) => dbContext.Set<TEntity>().Add(entity);
        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);
    }
}
