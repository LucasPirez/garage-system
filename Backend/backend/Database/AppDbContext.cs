using backend.Database.Entites;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        //protected override void OnConfiguring(DbContextOptions optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseNpgsql(connectionString);
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleEntry>().Property(k => k.Status).HasConversion<string>();
            var workshopId1 = Guid.NewGuid().ToString();
            var workshopId2 = Guid.NewGuid().ToString();
            modelBuilder
                .Entity<WorkShop>()
                .HasData(
                    new List<WorkShop>()
                    {
                        new WorkShop() { Id = workshopId1, Name = "Taller Jesuita" },
                        new WorkShop() { Id = workshopId2, Name = "Taller Silvana" },
                    }
                );

            modelBuilder
                .Entity<Customer>()
                .HasData(
                    new List<Customer>()
                    {
                        new Customer()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = "Juan ",
                            LastName = "Perez",
                            WorkShopId = workshopId1,
                            PhoneNumber = new List<string>() { "3424388239" },
                            Email = new List<string>() { "lucaspirez42@gmail.com" },
                        },
                        new Customer()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = "Maria ",
                            LastName = "Lopez",
                            WorkShopId = workshopId1,
                        },
                    }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WorkShop> WorkShops => Set<WorkShop>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<VehicleEntry> VehicleEntries => Set<VehicleEntry>();
        public DbSet<Payment> Payments => Set<Payment>();
    }
}
