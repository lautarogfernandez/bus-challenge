using BusApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Kid> Kids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.RegistrationPlate).IsUnique();

                entity.HasOne(x => x.Driver)
                      .WithOne(d => d.Bus)
                      .HasForeignKey<Bus>(x => x.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.DocumentNumber).IsUnique();
            });

            modelBuilder.Entity<Kid>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.DocumentNumber).IsUnique();

                entity.HasOne(x => x.Bus)
                      .WithMany(b => b.Kids)
                      .HasForeignKey(x => x.BusId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}