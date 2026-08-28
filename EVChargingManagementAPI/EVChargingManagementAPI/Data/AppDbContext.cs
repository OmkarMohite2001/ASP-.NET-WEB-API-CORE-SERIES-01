using EVChargingManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<ChargingStation> ChargingStations { get; set; }

        public DbSet<Charger> Chargers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<ChargingSession> ChargingSessions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChargingStation>()
                .HasMany(s => s.Chargers)
                .WithOne(c => c.ChargingStation)
                .HasForeignKey(c => c.ChargingStationId);


            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Customer)
                .HasForeignKey(v => v.CustomerId);


            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.ChargingSessions)
                .WithOne(s => s.Vehicle)
                .HasForeignKey(s => s.VehicleId);


            modelBuilder.Entity<Charger>()
                .HasMany(c => c.ChargingSessions)
                .WithOne(s => s.Charger)
                .HasForeignKey(s => s.ChargerId);
        }
    }
}
