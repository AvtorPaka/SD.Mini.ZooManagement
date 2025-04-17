using Microsoft.AspNetCore.Mvc;
using SD.Mini.ZooManagement.Api.Contracts.Requests.Animals;
using SD.Mini.ZooManagement.Api.Contracts.Responses.Animals;
using SD.Mini.ZooManagement.Api.FilterAttributes;
using SD.Mini.ZooManagement.Api.Mappers.Animals;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Api.Controllers;

[ApiController]
[Route("animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;
    private readonly IAnimalTransferService _animalTransferService;

    public AnimalsController(IAnimalService animalService, IAnimalTransferService animalTransferService)
    {
        _animalService = animalService;
        _animalTransferService = animalTransferService;
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType<AddAnimalResponse>(200)]
    [ErrorResponseType(400)]
    public async Task<IActionResult> AddAnimal(AddAnimalRequest request, CancellationToken cancellationToken)
    {
        EntityId createdId = await _animalService.AddNewAnimal(
            animalModel: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );

        return Ok(new AddAnimalResponse(Id: createdId.ToString()));
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType<GetAnimalResponse>(200)]
    [ErrorResponseType(400)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> GetAnimal([FromQuery] GetAnimalRequest request,
        CancellationToken cancellationToken)
    {
        AnimalModelContainer container = await _animalService.GetAnimal(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return Ok(container.MapContainerToResponse());
    }

    [HttpGet]
    [Route("get/all")]
    [ProducesResponseType<IEnumerable<GetAnimalResponse>>(200)]
    public async Task<IActionResult> GetAllAnimals(CancellationToken cancellationToken)
    {
        IReadOnlyList<AnimalModelContainer> containers = await _animalService.GetAnimals(cancellationToken);

        return Ok(containers.MapContainersToResponse());
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType<DeleteAnimalResponse>(200)]
    [ErrorResponseType(404)]
    public async Task<IActionResult> DeleteAnimal(DeleteAnimalRequest request, CancellationToken cancellationToken)
    {
        await _animalService.DeleteAnimal(
            id: new EntityId(request.Id),
            cancellationToken: cancellationToken
        );

        return Ok(new DeleteAnimalResponse());
    }
}