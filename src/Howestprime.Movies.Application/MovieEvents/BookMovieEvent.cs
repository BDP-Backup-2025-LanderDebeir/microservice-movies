using Domaincrafters.Application;
using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record BookMovieEventInput(
    string MovieEventId,
    int StandardVisitors,
    int DiscountVisitors
    );

public class BookMovieEvent(
    IMovieEventRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<BookMovieEvent> logger
    ) : IUseCase<BookMovieEventInput, Task<string>>
{
    private readonly IMovieEventRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<BookMovieEvent> _logger = logger;

    public async Task<string> Execute(BookMovieEventInput input)
    {
        MovieEventId movieEventId = new(input.MovieEventId);
        MovieEvent movieEvent = (await _repository.ById(movieEventId)).Value;

        if (movieEvent.Time.Year - DateTime.Now.Year < 0 || movieEvent.Time.Year - DateTime.Now.Year > 1 || movieEvent.Time.DayOfYear - DateTime.Now.DayOfYear >= 14)
            throw new InvalidOperationException($"Bookings for the event with id {movieEventId} haven't opened yet");

        Room room = (await _repository.GetRoomById(movieEvent.RoomId)).Value;

        Booking booking = Booking.Create(movieEventId, input.StandardVisitors, input.DiscountVisitors);
        movieEvent.Book(booking, room);
        await _repository.Save(movieEvent);
        await _unitOfWork.Do();

        _logger.LogInformation("booking {bookingId} booked", booking.Id);
        return booking.Id.Value;
    }
}
