using Domaincrafters.Application;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movie;

namespace Howestprime.Movies.Main.Modules;

public static class UseCaseServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddRegisterMovie();
    }

    private static IServiceCollection AddRegisterMovie(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<RegisterMovieInput, Task<string>>>(ServiceProvider =>
            {
                var movieRepository = ServiceProvider.GetRequiredService<IMovieRepository>();
                var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
                var logger = ServiceProvider.GetRequiredService<ILogger<RegisterMovie>>();

                return new RegisterMovie(movieRepository, unitOfWork, logger);
            });
    }
}