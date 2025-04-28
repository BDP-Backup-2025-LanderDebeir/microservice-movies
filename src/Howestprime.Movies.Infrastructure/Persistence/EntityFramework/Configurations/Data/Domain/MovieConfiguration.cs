using Howestprime.Movies.Domain.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data.Domain;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            value => new MovieId(value)
            );
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.Genres).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Actors).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.AgeRating).IsRequired();
        builder.Property(x => x.PosterUrl).IsRequired().HasMaxLength(1000);
    }
}
