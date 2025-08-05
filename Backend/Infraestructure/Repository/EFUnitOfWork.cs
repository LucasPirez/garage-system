using Domain.Common;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public EFUnitOfWork(AppDbContext context) => _context = context;

        public async Task BeginTransactionAsync() =>
            _transaction = await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("there isn't active transaction");

            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("there isn't active transaction");

            await _transaction.RollbackAsync();
        }
    }
}
