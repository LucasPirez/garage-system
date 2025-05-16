using AutoMapper;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;

namespace backend.Modules.CustomerModule
{
    public class CustomerService : Repository<Customer>
    {
        public CustomerService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }
    }
}
