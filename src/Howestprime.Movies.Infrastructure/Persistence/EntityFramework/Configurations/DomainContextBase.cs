using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;

public abstract class DomainContextBase : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<MovieEvent> MovieEvents { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new MovieEventConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Seed();
    }
}