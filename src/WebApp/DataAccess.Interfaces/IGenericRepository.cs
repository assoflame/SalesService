using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity>  where TEntity : class
    {
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
