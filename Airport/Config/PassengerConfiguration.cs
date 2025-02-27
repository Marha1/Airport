using Airport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            builder.Property(p => p.IsReg).HasDefaultValue(false);

            // Связь: 1 пассажир имеет 1 багаж
            builder.HasOne(p => p.Baggage)
                   .WithOne(b => b.Passenger)
                   .HasForeignKey<Baggage>(b => b.PassengerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
