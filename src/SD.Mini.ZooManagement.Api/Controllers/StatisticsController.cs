using Microsoft.AspNetCore.Mvc;
using SD.Mini.ZooManagement.Api.Contracts.Responses.Statistics;
using SD.Mini.ZooManagement.Api.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Containers.Statistics;
using SD.Mini.ZooManagement.Application.Services.Interfaces;

namespace SD.Mini.ZooManagement.Api.Controllers;

[ApiController]
[Route("statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IZooStatisticsService _zooStatisticsService;

    public StatisticsController(IZooStatisticsService zooStatisticsService)
    {
        _zooStatisticsService = zooStatisticsService;
    }

    [HttpGet]
    [Route("animals/count-types")]
    [ProducesResponseType<GetZooAnimalsCountResponse>(200)]
    public async Task<IActionResult> GetAnimalsCount(CancellationToken cancellationToken)
    {
        CountAnimalsTypeContainer container = await _zooStatisticsService.CountAnimalsTypes(cancellationToken);

        return Ok(
            new GetZooAnimalsCountResponse(
                TotalCount: container.TotalCount,
                TypesCount: container.TypeCountDict
            )
        );
    }

    [HttpGet]
    [Route("animals/enclosure-placement-rate")]
    [ProducesResponseType<GetZooEnclosurePlacementRateResponse>(200)]
    public async Task<IActionResult> GetEnclosurePlacementRate(CancellationToken cancellationToken)
    {
        decimal rate = await _zooStatisticsService.CalculateAnimalEnclosurePlacementRate(cancellationToken);

        return Ok(new GetZooEnclosurePlacementRateResponse(
            PlacementRatePercentages: rate
        ));
    }

    [HttpGet]
    [Route("enclosures/get-empty")]
    [ProducesResponseType<GetZooEmptyEnclosuresResponse>(200)]
    public async Task<IActionResult> GetEmptyEnclosures(CancellationToken cancellationToken)
    {
        var container = await _zooStatisticsService.GetEmptyEnclosures(cancellationToken);

        return Ok(new GetZooEmptyEnclosuresResponse(
            TotalCount: container.Count,
            Enclosures: container.Enclosures.MapContainersToResponses()
        ));
    }

    [HttpGet]
    [Route("enclosures/get-full")]
    [ProducesResponseType<GetZooFullEnclosuresResponse>(200)]
    public async Task<IActionResult> GetFillEnclosures(CancellationToken cancellationToken)
    {
        var container = await _zooStatisticsService.GetFullEnclosures(cancellationToken);

        return Ok(new GetZooFullEnclosuresResponse(
            TotalCount: container.Count,
            Enclosures: container.Enclosures.MapContainersToResponses()
        ));
    }
}