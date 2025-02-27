using Airport.Models;
using Airport.Models.Airport.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport.Data
{
    public class AirportDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Baggage> Baggage { get; set; }

        public AirportDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=Air;username=postgres;password=053352287");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.FlightConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PassengerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.BaggageConfiguration());
        }
    }
}