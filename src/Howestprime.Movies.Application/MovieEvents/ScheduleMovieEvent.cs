using Aornis;
using Domaincrafters.Application;
using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record ScheduleMovieEventInput(
    string MovieId,
    DateTime Date,
    string RoomId
    );

public class ScheduleMovieEvent(
    IMovieEventRepository movieEventRepository,
    IMovieRepository movieRepository,
    IUnitOfWork unitOfWork,
    ILogger<ScheduleMovieEvent> logger
    ) : IUseCase<ScheduleMovieEventInput, Task<string>>
{
    private readonly IMovieEventRepository _movieEventRepository = movieEventRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<ScheduleMovieEvent> _logger = logger;

    public async Task<string> Execute(ScheduleMovieEventInput input)
    {
        MovieId movieId = new MovieId(input.MovieId);
        RoomId roomId = new RoomId(input.RoomId);
        DateTime time = input.Date;

        Movie movie = (await _movieRepository.ById(movieId)).Value;
        Room room = (await _movieEventRepository.GetRoomById(roomId)).Value;
        Optional<MovieEvent> existingEvent = await _movieEventRepository.FindByTimeAndRoom(input.Date, roomId);

        if (existingEvent.HasValue)
            await _movieEventRepository.Remove(existingEvent.Value);

        MovieEvent movieEvent = MovieEvent.Create(movieId, roomId, time, room.Capacity);
        await _movieEventRepository.Save(movieEvent);
        await _unitOfWork.Do();

        return movieEvent.Id.Value;
    }
}

