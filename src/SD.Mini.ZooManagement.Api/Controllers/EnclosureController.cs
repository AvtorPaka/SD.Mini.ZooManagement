using Microsoft.AspNetCore.Mvc;
using SD.Mini.ZooManagement.Api.Contracts.Requests.Enclosure;
using SD.Mini.ZooManagement.Api.Contracts.Responses.Enclosure;
using SD.Mini.ZooManagement.Api.FilterAttributes;
using SD.Mini.ZooManagement.Api.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Api.Controllers;

[ApiController]
[Route("enclosures")]
public class EnclosureController : ControllerBase
{
    private readonly IEnclosureService _enclosureService;
    private readonly IAnimalTransferService _animalTransferService;

    public EnclosureController(IEnclosureService enclosureService, IAnimalTransferService animalTransferService)
    {
        _enclosureService = enclosureService;
        _animalTransferService = animalTransferService;
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType<AddEnclosureResponse>(200)]
    [ErrorResponseType(400)]
    public async Task<IActionResult> Add(AddEnclosureRequest request, CancellationToken cancellationToken)
    {
        EntityId createdId = await _enclosureService.AddNewEnclosure(
            model: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );

        return Ok(new AddEnclosureResponse(Id: createdId.ToString()));
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType<GetEnclosureResponse>(200)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> Get([FromQuery] GetEnclosureRequest request, CancellationToken cancellationToken)
    {
        var container = await _enclosureService.GetEnclosure(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return Ok(container.MapContainerToResponse());
    }

    [HttpGet]
    [Route("get/all")]
    [ProducesResponseType<IEnumerable<GetEnclosureResponse>>(200)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var containers = await _enclosureService.GetEnclosures(cancellationToken);

        return Ok(containers.MapContainersToResponses());
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType<DeleteEnclosureResponse>(200)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> Delete(DeleteEnclosureRequest request, CancellationToken cancellationToken)
    {
        await _enclosureService.DeleteEnclosure(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return Ok(new DeleteEnclosureResponse());
    }

    [HttpDelete]
    [Route("delete/animal")]
    [ProducesResponseType<RemoveAnimalFromEnclosureResponse>(200)]
    [ErrorResponseType(400)]
    [ErrorResponseType(404)]
    [ErrorResponseType(409)]
    public async Task<IActionResult> RemoveFromEnclosure(RemoveAnimalFromEnclosureRequest request,
        CancellationToken cancellationToken)
    {
        await _animalTransferService.RemoveAnimalFromEnclosure(
            enclosureId: new EntityId(request.EnclosureId),
            animalId: new EntityId(request.AnimalId),
            cancellationToken: cancellationToken
        );

        return Ok(new RemoveAnimalFromEnclosureResponse());
    }
}