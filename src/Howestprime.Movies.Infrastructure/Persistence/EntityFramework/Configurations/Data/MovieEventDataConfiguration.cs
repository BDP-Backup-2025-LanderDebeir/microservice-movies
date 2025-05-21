using Howestprime.Movies.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;

public sealed class MovieEventDataConfiguration : IEntityTypeConfiguration<MovieEventData>
{
    public void Configure(EntityTypeBuilder<MovieEventData> builder)
    {
        builder.ToTable("MovieEvents");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Time).HasColumnType("timestamp(6)");

        builder.Property(x => x.RoomId);

        builder.Property(x => x.MovieId);

        builder.HasOne(x => x.Movie).WithMany(m => m.Events).HasForeignKey(x => x.MovieId);
        builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.RoomId);
    }
}
