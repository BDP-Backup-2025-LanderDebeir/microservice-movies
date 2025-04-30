namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MovieEventsDTO(
    IEnumerable<MovieEventDetailDTO> Data
    );