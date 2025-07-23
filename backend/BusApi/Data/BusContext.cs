using BusApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Data
{
    public class BusContext : DbContext
    {
        public BusContext(DbContextOptions<BusContext> options) : base(options)
        {

        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Kid> Kids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bus>().HasKey(x => x.Id);

            modelBuilder.Entity<Bus>()
                .HasOne(b => b.Driver)
                .WithOne(d => d.Bus)
                .HasForeignKey<Bus>(b => b.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                .HasMany(b => b.Kids)
                .WithMany(k => k.Buses);

            modelBuilder.Entity<Bus>()
                .HasIndex(b => b.RegistrationPlate)
                .IsUnique();

            modelBuilder.Entity<Driver>().HasKey(x => x.Id);

            modelBuilder.Entity<Driver>()
                .HasIndex(b => b.DocumentNumber)
                .IsUnique();

            modelBuilder.Entity<Kid>().HasKey(x => x.Id);

            modelBuilder.Entity<Kid>()
                .HasIndex(b => b.DocumentNumber)
                .IsUnique();
        }
    }
}