namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MovieEventDetailDTO(
    Guid Id,
    RoomDto Room,
    DateTime Time,
    MovieDTO Movie,
    int Capacity
    );
