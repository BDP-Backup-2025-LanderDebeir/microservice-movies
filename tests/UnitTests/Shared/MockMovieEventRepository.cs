using Aornis;
using Howestprime.Movies.Domain.MovieEvent;

namespace UnitTests.Shared;

public sealed class MockMovieEventRepository : IMovieEventRepository
{
    public List<MovieEvent> SavedMovieEvents { get; } = new List<MovieEvent>();
    public List<MovieEvent> MovieEvents { get; } = new List<MovieEvent>();
    public List<Room> Rooms { get; } = new List<Room>();
    public Exception? ThrowOnSave { get; set; }

    public Task<Optional<MovieEvent>> ById(MovieEventId id)
    {
        var movieEvent = MovieEvents.SingleOrDefault(m => m.Id == id);
        return Task.FromResult(Optional.Of(movieEvent));
    }

    public Task<Optional<MovieEvent>> FindByTimeAndRoom(DateTime time, RoomId roomId)
    {
        var movieEvent = MovieEvents.FirstOrDefault(e => e.Time.Hour == time.Hour && e.RoomId == roomId);
        return Task.FromResult(Optional.Of(movieEvent));
    }

    public Task<Optional<Room>> GetRoomById(RoomId id)
    {
        var room = Rooms.SingleOrDefault(m => m.Id == id);
        return Task.FromResult(Optional.Of(room));
    }

    public Task Remove(MovieEvent entity)
    {
        MovieEvents.Remove(entity);
        return Task.CompletedTask;
    }

    public Task Save(MovieEvent entity)
    {
        if (ThrowOnSave != null)
            throw ThrowOnSave;

        SavedMovieEvents.Add(entity);

        var existingIndex = MovieEvents.FindIndex(m => m.Id == entity.Id);

        if (existingIndex >= 0)
            MovieEvents[existingIndex] = entity;
        else
            MovieEvents.Add(entity);

        return Task.CompletedTask;
    }
}