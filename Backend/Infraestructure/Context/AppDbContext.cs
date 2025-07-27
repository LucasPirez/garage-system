using Domain;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Context
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
            modelBuilder.Entity<Admin>().HasIndex(k => k.Email).IsUnique();

            modelBuilder.Entity<Vehicle>().HasIndex(k => k.Plate).IsUnique();

            modelBuilder.Entity<RepairOrder>().Property(k => k.Status).HasConversion<string>();

            modelBuilder
                .Entity<WorkShop>()
                .HasData(
                    new List<WorkShop>()
                    {
                        new WorkShop()
                        {
                            Id = new Guid(SeedData.workshopAId),
                            Name = "Taller Jesus",
                        },
                        new WorkShop()
                        {
                            Id = new Guid(SeedData.workshopBId),
                            Name = "Taller Silvana",
                        },
                    }
                );

            modelBuilder
                .Entity<Admin>()
                .HasData(
                    new List<Admin>()
                    {
                        new Admin()
                        {
                            Email = "email@gmail.com",
                            Id = Guid.NewGuid(),
                            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                            WorkShopId = new Guid(SeedData.workshopAId),
                        },
                        new Admin()
                        {
                            Email = "email2@gmail.com",
                            Id = Guid.NewGuid(),
                            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                            WorkShopId = new Guid(SeedData.workshopBId),
                        },
                    }
                );

            modelBuilder
                .Entity<EFCustomer>()
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
        public DbSet<EFCustomer> Customers => Set<EFCustomer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<RepairOrder> VehicleEntries => Set<RepairOrder>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Admin> Admins => Set<Admin>();
    }
}
