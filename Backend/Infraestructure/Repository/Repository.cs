using System.Linq.Expressions;
using AutoMapper;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class WriteRepository<T, EFEntity> : IWriteRepository<T>
        where EFEntity : Base
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<EFEntity> _dbSet;
        protected readonly IMapper _mapper;

        public WriteRepository(AppDbContext database, IMapper mapper)
        {
            _context = database;
            _dbSet = database.Set<EFEntity>();
            _mapper = mapper;
        }

        public async Task CreateAsync(T entity)
        {
            EFEntity efEntity = _mapper.Map<EFEntity>(entity);

            var response = await _dbSet.AddAsync(efEntity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            EFEntity efEntity = _mapper.Map<EFEntity>(entity);
            var local = _dbSet.Local.FirstOrDefault(entry => entry.Id.Equals(efEntity.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _dbSet.Remove(efEntity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                EFEntity efEntity = _mapper.Map<EFEntity>(entity);
                var local = _dbSet.Local.FirstOrDefault(entry => entry.Id.Equals(efEntity.Id));

                if (local != null)
                {
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(efEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException("Error updating entity in the database, ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity, ", ex);
            }
        }
    }

    public class ReadRepository<T> : IReadRepository<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public ReadRepository(AppDbContext database)
        {
            _context = database;
            _dbSet = database.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }
    }

    public class ReadRangeRepository<T> : IReadRangeRepository<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public ReadRangeRepository(AppDbContext database)
        {
            _context = database;
            _dbSet = database.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An unexpected error occurred while retrieving all entities, ",
                    ex
                );
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (where != null)
                {
                    query = query.Where(where);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An unexpected error occurred while retrieving all entities, ",
                    ex
                );
            }
        }
    }
}
