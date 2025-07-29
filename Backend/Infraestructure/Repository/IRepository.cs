using System.Linq.Expressions;

namespace Infraestructure.Repository
{
    public interface IReadRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
    }

    public interface IWriteRepository<T>
    {
        Task CreateAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }

    public interface IReadRangeRepository<T>
    {
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where);
    }
}
