using SD.Mini.ZooManagement.Api.Contracts.Responses.Enclosure;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses.Statistics;

public record GetZooEmptyEnclosuresResponse(
    int TotalCount,
    IEnumerable<GetEnclosureResponse> Enclosures
);