using System.Linq.Expressions;

namespace backend.Database.Repository
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T?> GetById(int id);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task Delete(T entity);

        Task Update(T entity);

        Task<T> AddWithDto<DTO>(DTO Dto);
    }
}
