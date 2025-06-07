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
            modelBuilder.Entity<RepairOrder>().Property(k => k.Status).HasConversion<string>();
            modelBuilder
                .Entity<WorkShop>()
                .HasData(
                    new List<WorkShop>()
                    {
                        new WorkShop()
                        {
                            Id = new Guid(SeedData.workshopAId),
                            Name = "Taller Jesuita",
                        },
                        new WorkShop()
                        {
                            Id = new Guid(SeedData.workshopBId),
                            Name = "Taller Silvana",
                        },
                    }
                );

            modelBuilder
                .Entity<Customer>()
                .HasData(
                    new List<Customer>()
                    {
                        new Customer()
                        {
                            Id = new Guid(SeedData.customerAId),
                            FirstName = "Juan ",
                            LastName = "Perez",
                            WorkShopId = new Guid(SeedData.workshopAId),
                            PhoneNumber = new List<string>() { "3424388239" },
                            Email = new List<string>() { "lucaspirez42@gmail.com" },
                        },
                        new Customer()
                        {
                            Id = new Guid(SeedData.customerBId),
                            FirstName = "Maria ",
                            LastName = "Lopez",
                            WorkShopId = new Guid(SeedData.workshopAId),
                        },
                    }
                );
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RepairOrder>().OwnsMany(k => k.SpareParts);
        }

        public DbSet<WorkShop> WorkShops => Set<WorkShop>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<RepairOrder> VehicleEntries => Set<RepairOrder>();
        public DbSet<Payment> Payments => Set<Payment>();
    }
}
