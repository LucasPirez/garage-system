using System.Linq.Expressions;

namespace Infraestructure.Repository
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T?> GetById(Guid id);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? where);

        Task Delete(T entity);

        Task Update(T entity);

        Task<T> AddWithDto<DTO>(DTO Dto);
    }
}
