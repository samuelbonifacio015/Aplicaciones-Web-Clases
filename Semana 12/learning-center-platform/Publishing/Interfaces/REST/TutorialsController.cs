using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST;

/// <summary>
///     The Tutorials Controller
/// </summary>
/// <param name="tutorialQueryService">
///     The <see cref="ITutorialQueryService"/> instance to query tutorials
/// </param>
/// <param name="tutorialCommandService">
///     The <see cref="ITutorialCommandService"/> instance to manage tutorial commands
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Tutorial endpoints")]
public class TutorialsController(
    ITutorialQueryService tutorialQueryService,
    ITutorialCommandService tutorialCommandService) : ControllerBase
{ 
    /// <summary>
    ///     Get Tutorial By Id
    /// </summary>
    /// <param name="tutorialId">
    ///     The Tutorial Id
    /// </param>
    /// <returns>
    ///     The <see cref="TutorialResource"/> with the tutorial if found, otherwise a NotFound response
    /// </returns>
    [HttpGet("{tutorialId:int}")]
    [SwaggerOperation(
        Summary = "Get Tutorial By Id",
        Description = "Get a tutorial by its unique identifier",
        OperationId = "GetTutorialById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The tutorial was found", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The tutorial was not found")]
    public async Task<IActionResult> GetAllTutorialById([FromRoute] int tutorialId)
    {
        var tutorial = await tutorialQueryService.Handle(new GetTutorialByIdQuery(tutorialId));
        if (tutorial is null) return  NotFound();
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return Ok(tutorialResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Tutorials",
        Description = "Get all Tutorials",
        OperationId = "GetAllTutorials"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The tutorials were found", typeof(IEnumerable<TutorialResource>))]
    public async Task<IActionResult> GetAllTutorials()
    {
        var tutorials = await tutorialQueryService.Handle(new GetAllTutorialsQuery());
        var tutorialResources = tutorials.Select(
            TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
    /// <summary>
    ///     Create a new Tutorial
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateTutorialResource"/> with the tutorial data
    /// </param>
    /// <returns>
    ///     The <see cref="TutorialResource"/> with the created tutorial if successful, otherwise a BadRequest response
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Tutorial",
        Description = "Create a new Tutorial",
        OperationId = "CreateTutorial"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The tutorial was created", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The tutorial was not created")]
    public async Task<IActionResult> CreateTutorial([FromBody] CreateTutorialResource resource)
    {
        var createTutorialCommand = CreateTutorialCommandFromResourceAssembler.ToCommandFromResource(resource);
        var createdTutorial = await tutorialCommandService.Handle(createTutorialCommand);
        if (createdTutorial is null) return BadRequest("Tutorial could not be created.");
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(createdTutorial);
        return CreatedAtAction(nameof(GetAllTutorialById), new { tutorialId = tutorialResource.Id }, tutorialResource);
    }
    
    
    [HttpPost("{tutorialId:int}/videos")]
    [SwaggerOperation(
        Summary = "Add a video to a Tutorial",
        Description = "Add a video to a Tutorial",
        OperationId = "AddVideoTutorial"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The video was added to the tutorial", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The video was not added to the tutorial")]
    public async Task<IActionResult> AddVideoToTutorial(
        [FromBody] AddVideoAssetToTutorialResource resource,
        [FromRoute] int tutorialId)
    {
        var addVideoAssetToTutorialCommand = AddVideoAssetToTutorialCommandFromResourceAssembler
            .ToCommandFromResource(resource, tutorialId);
        var tutorial = await tutorialCommandService.Handle(addVideoAssetToTutorialCommand);
        if (tutorial is null) return BadRequest("Tutorial or Video Asset not found.");
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetAllTutorialById), new { tutorialId = tutorialResource.Id }, tutorialResource);
            
    }
    
    
    
    
    
}