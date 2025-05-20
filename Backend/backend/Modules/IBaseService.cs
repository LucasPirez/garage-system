namespace backend.Modules
{
    public interface IServiceBase<T, CreateDto>
    {
        Task<IEnumerable<T>> GetAllAsync(Guid workshopId);
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(CreateDto createDto);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
