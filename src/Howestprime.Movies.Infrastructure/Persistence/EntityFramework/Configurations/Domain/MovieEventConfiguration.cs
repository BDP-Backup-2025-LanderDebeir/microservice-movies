using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;

public sealed class MovieEventConfiguration : IEntityTypeConfiguration<MovieEvent>
{
    public void Configure(EntityTypeBuilder<MovieEvent> builder)
    {
        builder.ToTable("MovieEvents");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            value => new MovieEventId(value)
            );

        builder.Property(x => x.MovieId).ValueGeneratedNever();
        builder.Property(x => x.MovieId).HasConversion(
            id => id.Value,
            value => new MovieId(value)
            );
        builder.HasOne("Movies").WithMany().HasForeignKey("MovieId");

        builder.Property(x => x.RoomId).ValueGeneratedNever();
        builder.Property(x => x.RoomId).HasConversion(
            id => id.Value,
            value => new RoomId(value)
            );
        builder.HasOne("Rooms").WithMany().HasForeignKey("RoomId");

        builder.Property(x => x.Time);
        builder.Property(x => x.Visitors);
        builder.Property(x => x.Capacity);
    }
}
