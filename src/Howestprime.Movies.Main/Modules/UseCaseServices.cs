using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;

namespace Howestprime.Movies.Main.Modules;

public static class UseCaseServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddRegisterMovie()
            .AddFindMovieByFilter()
            .AddFindMovieById()
            .AddScheduleMovieEvent()
            .AddFindMovieByIdWithEvents()
            .AddFindMovieEventsForMonth();
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

    private static IServiceCollection AddScheduleMovieEvent(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<ScheduleMovieEventInput, Task<string>>>(ServiceProvider =>
            {
                var movieEventRepository = ServiceProvider.GetRequiredService<IMovieEventRepository>();
                var movieRepository = ServiceProvider.GetRequiredService<IMovieRepository>();
                var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
                var logger = ServiceProvider.GetRequiredService<ILogger<ScheduleMovieEvent>>();
                return new ScheduleMovieEvent(movieEventRepository, movieRepository, unitOfWork, logger);
            });
    }

    private static IServiceCollection AddFindMovieByIdWithEvents(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<FindMovieByIdWithEventsInput, Task<MovieData>>>(ServiceProvider =>
            {
                var query = ServiceProvider.GetRequiredService<IFindMovieByIdWithEventsQuery>();
                return new FindMovieByIdWithEvents(query);
            });
    }

    private static IServiceCollection AddFindMovieEventsForMonth(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<FindMovieEventsForMonthInput, Task<IReadOnlyList<MovieEventData>>>>(ServiceProvider =>
            {
                var query = ServiceProvider.GetRequiredService<IAllMovieEventsQuery>();
                return new FindMovieEventsForMonth(query);
            });
    }
}