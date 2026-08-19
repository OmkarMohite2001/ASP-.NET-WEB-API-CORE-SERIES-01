using Microsoft.EntityFrameworkCore;
using ComponyRegistrationAPI.Models;
namespace ComponyRegistrationAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options)
        {
           
        }
        public DbSet<Compony> Componies { get; set; }
        public DbSet<ComponyRegistration> ComponyRegistrations { get; set; }
        public DbSet<ComponyBranch> ComponyBranches { get; set; }
        public DbSet<Service> services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compony>()
             .HasOne(c => c.Registration)
             .WithOne(r => r.Compony)
             .HasForeignKey<ComponyRegistration>(r => r.ComponyId);

            modelBuilder.Entity<ComponyRegistration>()
                .HasIndex(r => r.ComponyId)
                .IsUnique();

            modelBuilder.Entity<Compony>()
                .HasMany(c => c.Branches)
                .WithOne(b => b.Compony)
                .HasForeignKey(b => b.CompanyId);
            modelBuilder.Entity<Compony>()
                .HasMany(c => c.services)
                .WithMany(c => c.Componies);
        }

    }
}
