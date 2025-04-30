using Howestprime.Movies.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;

public sealed class RoomDataConfiguration : IEntityTypeConfiguration<RoomData>
{
    public void Configure(EntityTypeBuilder<RoomData> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Name);
        builder.Property(x => x.Capacity);
    }
}
