using Aornis;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public sealed class EFMovieEventRepository(
    DomainContextBase context
    ) : IMovieEventRepository
{
    private readonly DomainContextBase _context = context;

    public Task<Optional<MovieEvent>> ById(MovieEventId id)
    {
        return _context.MovieEvents
            .Include(e => e.Bookings)
            .SingleOrDefaultAsync(e => e.Id == id)
            .ContinueWith(task => Optional.Of(task.Result));
    }

    public Task<Optional<MovieEvent>> FindByTimeAndRoom(DateTime time, RoomId roomId)
    {
        return _context.MovieEvents
            .FirstOrDefaultAsync(e => e.Time.Hour == time.Hour && e.RoomId == roomId)
            .ContinueWith(task => Optional.Of(task.Result));
    }

    public Task<Optional<Room>> GetRoomById(RoomId id)
    {
        return _context.Rooms
            .SingleOrDefaultAsync(r => r.Id == id)
            .ContinueWith(task => Optional.Of(task.Result));
    }

    public Task Remove(MovieEvent entity)
    {
        _context.MovieEvents.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task Save(MovieEvent entity)
    {
        bool exists = await _context.MovieEvents.AnyAsync(e => e.Id == entity.Id);
        if (exists)
        {
            context.MovieEvents.Update(entity);
            return;
        }

        _context.MovieEvents.Add(entity);
    }
}
