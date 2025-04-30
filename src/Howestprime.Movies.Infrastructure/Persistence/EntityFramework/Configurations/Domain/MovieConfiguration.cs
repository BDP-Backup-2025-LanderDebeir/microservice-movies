using Howestprime.Movies.Domain.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;

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
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.Genres).IsRequired();
        builder.Property(x => x.Actors).IsRequired();
        builder.Property(x => x.AgeRating).IsRequired();
        builder.Property(x => x.PosterUrl).IsRequired();
    }
}
