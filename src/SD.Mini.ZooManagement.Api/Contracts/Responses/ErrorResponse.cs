using System.Net;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses;

public record ErrorResponse(
    HttpStatusCode StatusCode,
    string? Message,
    IEnumerable<string> Exceptions
);