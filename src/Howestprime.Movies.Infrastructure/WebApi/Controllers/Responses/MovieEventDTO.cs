namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MovieEventDto(
    Guid Id,
    RoomDto Room,
    DateTime Time,
    Guid MovieId
    );