using Aornis;
using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.MovieEvent;

public interface IMovieEventRepository : IRepository<MovieEvent, MovieEventId>
{
    Task<Optional<Room>> GetRoomById(RoomId id);
}
