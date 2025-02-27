using Airport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Configurations
{
    public class BaggageConfiguration : IEntityTypeConfiguration<Baggage>
    {
        public void Configure(EntityTypeBuilder<Baggage> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Weight).IsRequired();
            builder.Property(b => b.Length).IsRequired();
            builder.Property(b => b.Width).IsRequired();
            builder.Property(b => b.Height).IsRequired();
        }
    }
}
