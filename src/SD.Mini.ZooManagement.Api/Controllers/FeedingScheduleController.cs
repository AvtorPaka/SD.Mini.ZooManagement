using Microsoft.AspNetCore.Mvc;
using SD.Mini.ZooManagement.Api.Contracts.Requests.FeedingSchedule;
using SD.Mini.ZooManagement.Api.Contracts.Responses.FeedingSchedule;
using SD.Mini.ZooManagement.Api.FilterAttributes;
using SD.Mini.ZooManagement.Api.Mappers.FeedingSchedule;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Api.Controllers;

[ApiController]
[Route("feeding-schedules")]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingOrganizationService _feedingOrganizationService;

    public FeedingScheduleController(IFeedingOrganizationService feedingOrganizationService)
    {
        _feedingOrganizationService = feedingOrganizationService;
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType<AddFeedingScheduleResponse>(200)]
    [ErrorResponseType(400)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> Add(AddFeedingScheduleRequest request, CancellationToken cancellationToken)
    {
        EntityId createdId = await _feedingOrganizationService.AddFeedingSchedule(
            container: request.MapRequestToContainer(),
            cancellationToken: cancellationToken
        );

        return Ok(
            new AddFeedingScheduleResponse(
                Id: createdId.ToString()
            )
        );
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType<GetFeedingScheduleResponse>(200)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> Get([FromQuery] GetFeedingScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var containers = await _feedingOrganizationService.GetFeedingSchedule(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return Ok(containers.MapContainerToGeneralResponse());
    }

    [HttpGet]
    [Route("get/all")]
    [ProducesResponseType<IEnumerable<GetFeedingScheduleResponse>>(200)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var containers = await _feedingOrganizationService.GetFeedingSchedules(
            cancellationToken: cancellationToken
        );

        return Ok(containers.MapContainersToGeneralResponses());
    }

    [HttpGet]
    [Route("get/by-animal")]
    [ProducesResponseType<IEnumerable<GetAnimalFeedingScheduleResponse>>(200)]
    public async Task<IActionResult> GetPersonal([FromQuery]GetAnimalFeedingScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var containers = await _feedingOrganizationService.GetAnimalFeedingSchedule(
            animalId: new EntityId(request.AnimalId),
            cancellationToken: cancellationToken
        );

        return Ok(containers.MapContainersToPersonalResponses());
    }

    [HttpPatch]
    [Route("mark-done")]
    [ProducesResponseType<MarkFeedingScheduleDoneResponse>(200)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> MarkScheduleDone(MarkFeedingScheduleDoneRequest request,
        CancellationToken cancellationToken)
    {
        await _feedingOrganizationService.MarkFeedingScheduleDone(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return  Ok(new MarkFeedingScheduleDoneResponse());
    }
}