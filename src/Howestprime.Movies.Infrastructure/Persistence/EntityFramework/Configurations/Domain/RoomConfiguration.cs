using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            value => new RoomId(value)
            );

        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Capacity);
    }
}
