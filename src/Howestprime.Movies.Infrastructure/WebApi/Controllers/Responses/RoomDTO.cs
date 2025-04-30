namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record RoomDto(
    Guid Id,
    string Name,
    int Capacity
    );
