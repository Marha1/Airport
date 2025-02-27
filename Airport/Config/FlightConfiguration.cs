using Airport.Models;
using Airport.Models.Airport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.FlightNumber).IsRequired().HasMaxLength(10);
            builder.Property(f => f.DepartureAirport).IsRequired().HasMaxLength(50);
            builder.Property(f => f.ArrivalAirport).IsRequired().HasMaxLength(50);
            builder.Property(f => f.Status).HasMaxLength(20);

            // Связь: 1 рейс имеет много пассажиров
            builder.HasMany(f => f.Passengers)
                   .WithOne(p => p.Flight)
                   .HasForeignKey(p => p.FlightId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
