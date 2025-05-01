using Domaincrafters.Application;
using Howestprime.Movies.Domain.MovieEvent;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record BookMovieEventInput(
    string MovieEventId,
    int StandardVisitors,
    int DiscountVisitors
    );

public class BookMovieEvent(
    IMovieEventRepository repository,
    IUnitOfWork unitOfWork
    ) : IUseCase<BookMovieEventInput, Task<string>>
{
    private readonly IMovieEventRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<string> Execute(BookMovieEventInput input)
    {
        MovieEventId movieEventId = new(input.MovieEventId);
        MovieEvent movieEvent = (await _repository.ById(movieEventId)).Value;

        if (movieEvent.Time.Year - DateTime.Now.Year < 0 || movieEvent.Time.Year - DateTime.Now.Year > 1 || movieEvent.Time.DayOfYear - DateTime.Now.DayOfYear >= 14)
            throw new InvalidOperationException($"Bookings for the event with id {movieEventId} haven't opened yet");

        Booking booking = Booking.Create(movieEventId, input.StandardVisitors, input.DiscountVisitors);
        movieEvent.Book(booking);
        await _repository.Save(movieEvent);
        await _unitOfWork.Do();

        return booking.Id.Value;
    }
}
