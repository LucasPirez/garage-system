using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly IMapper? _mapper;

        public Repository(AppDbContext database)
        {
            _context = database;
            _dbSet = database.Set<T>();
        }

        public Repository(AppDbContext database, IMapper mapper)
        {
            _context = database;
            _dbSet = database.Set<T>();
            _mapper = mapper;
        }

        public async Task<T> AddWithDto<CreateDTO>(CreateDTO Dto)
        {
            if (_mapper is null)
                throw new Exception($"The Mapper is in {nameof(AddWithDto)} null here");

            T entity = _mapper.Map<T>(Dto);

            T entityCreated = await Add(entity);

            return entityCreated;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                var response = await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                return response.Entity;
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException(
                    "Error creating entity in the database, ",
                    dbEx
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An unexpected error occurred while creating the entity, ",
                    ex
                );
            }
        }

        public async Task<List<T>> Add(List<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException(
                    "Error creating entities in the database, ",
                    dbEx
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An unexpected error occurred while creating the entities, ",
                    ex
                );
            }
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetById(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An unexpected error occurred while retrieving the entity by ID, ",
                    ex
                );
            }
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
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

        public async Task Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
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
}
