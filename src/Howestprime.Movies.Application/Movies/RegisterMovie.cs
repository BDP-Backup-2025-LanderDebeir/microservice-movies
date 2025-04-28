using Domaincrafters.Application;
using Howestprime.Movies.Domain.Movie;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.Movies;

public sealed record RegisterMovieInput
    (
    string Title,
    string Description,
    int Duration,
    string Genre,
    int Year,
    string Actors,
    int AgeRating,
    string PosterUrl
    );

public sealed class RegisterMovie(
    IMovieRepository movieRepository,
    IUnitOfWork unitOfWork,
    ILogger<RegisterMovie> logger
    ) : IUseCase<RegisterMovieInput, Task<string>>
{
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger _logger = logger;
    public async Task<string> Execute(RegisterMovieInput input)
    {
        Movie movie = Movie.Create(input.Title, input.Description, input.Year, input.Duration, input.Genre, input.Actors, input.AgeRating, input.PosterUrl);

        await _movieRepository.Save(movie);
        await _unitOfWork.Do();

        _logger.LogInformation("Movie {MovieId} registered", movie.Id);

        return movie.Id.Value;
    }
}
