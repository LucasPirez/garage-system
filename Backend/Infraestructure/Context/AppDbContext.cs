using BCrypt.Net;
using Domain;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EFAdmin>().HasIndex(k => k.Email).IsUnique();

            modelBuilder.Entity<EFVehicle>().HasIndex(k => k.Plate).IsUnique();

            modelBuilder.Entity<EFRepairOrder>().Property(k => k.Status).HasConversion<string>();

            modelBuilder
                .Entity<EFWorkShop>()
                .HasData(
                    new List<EFWorkShop>()
                    {
                        new EFWorkShop()
                        {
                            Id = new Guid(SeedData.workshopAId),
                            Name = "Taller Jesus",
                        },
                        new EFWorkShop()
                        {
                            Id = new Guid(SeedData.workshopBId),
                            Name = "Taller Silvana",
                        },
                    }
                );

            modelBuilder
                .Entity<EFAdmin>()
                .HasData(
                    new List<EFAdmin>()
                    {
                        new EFAdmin()
                        {
                            Email = "email@gmail.com",
                            Id = Guid.NewGuid(),
                            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                            WorkShopId = new Guid(SeedData.workshopAId),
                        },
                        new EFAdmin()
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
                    new List<EFCustomer>()
                    {
                        new EFCustomer()
                        {
                            Id = new Guid(SeedData.customerAId),
                            FirstName = "Juan ",
                            LastName = "Perez",
                            WorkShopId = new Guid(SeedData.workshopAId),
                            PhoneNumber = new List<string>() { "3424388239" },
                            Email = new List<string>() { "lucaspirez42@gmail.com" },
                        },
                        new EFCustomer()
                        {
                            Id = new Guid(SeedData.customerBId),
                            FirstName = "Maria ",
                            LastName = "Lopez",
                            WorkShopId = new Guid(SeedData.workshopAId),
                        },
                    }
                );
            modelBuilder.Entity<EFRepairOrder>().OwnsMany(k => k.SpareParts);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EFWorkShop> WorkShops => Set<EFWorkShop>();
        public DbSet<EFCustomer> Customers => Set<EFCustomer>();

        public DbSet<EFVehicle> Vehicles => Set<EFVehicle>();

        public DbSet<EFRepairOrder> VehicleEntries => Set<EFRepairOrder>();

        //public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<EFAdmin> Admins => Set<EFAdmin>();
    }
}
