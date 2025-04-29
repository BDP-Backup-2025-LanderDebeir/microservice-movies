using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movie;

namespace Howestprime.Movies.Main.Modules;

public static class UseCaseServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddRegisterMovie()
            .AddFindMovieByFilter()
            .AddFindMovieById();
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

    private static IServiceCollection AddFindMovieByFilter(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<FindMovieByFilterInput, Task<IReadOnlyList<MovieData>>>>(ServiceProvider =>
            {
                var query = ServiceProvider.GetRequiredService<IFindMovieQuery>();
                return new FindMovieByFilter(query);
            });
    }

    private static IServiceCollection AddFindMovieById(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<FindMovieByIdInput, Task<MovieData?>>>(ServiceProvider =>
            {
                var query = ServiceProvider.GetRequiredService<IFindMovieByIdQuery>();
                return new FindMovieById(query);
            });
    }
}