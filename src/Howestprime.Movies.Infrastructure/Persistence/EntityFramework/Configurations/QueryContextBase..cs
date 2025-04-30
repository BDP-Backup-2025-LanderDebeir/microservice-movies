using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;

public abstract class QueryContextBase : DbContext
{
    public DbSet<MovieData> Movies { get; set; }
    public DbSet<RoomData> Rooms { get; set; }
    public DbSet<MovieEventData> MovieEvents { get; set; }

    protected QueryContextBase()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieDataConfiguration());
        modelBuilder.ApplyConfiguration(new RoomDataConfiguration());
        modelBuilder.ApplyConfiguration(new MovieEventDataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
