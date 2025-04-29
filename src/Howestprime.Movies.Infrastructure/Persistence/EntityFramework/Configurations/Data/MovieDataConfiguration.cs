using Howestprime.Movies.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;

public sealed class MovieDataConfiguration : IEntityTypeConfiguration<MovieData>
{
    public void Configure(EntityTypeBuilder<MovieData> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.Title);
        builder.Property(x => x.Year);
        builder.Property(x => x.Actors);
        builder.Property(x => x.PosterUrl);
        builder.Property(x => x.AgeRating);
        builder.Property(x => x.Description);
        builder.Property(x => x.Duration);
        builder.Property(x => x.Genre)
            .HasColumnName("Genres");
    }
}