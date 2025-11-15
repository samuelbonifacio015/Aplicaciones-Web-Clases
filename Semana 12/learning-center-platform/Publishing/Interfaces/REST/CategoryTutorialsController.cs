using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/categories/{categoryId:int}/tutorials")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Categories")]
public class CategoryTutorialsController( ITutorialQueryService tutorialQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Tutorials By category Id",
        Description =  "Get all Tutorials By category Id",
        OperationId = "GetAllTutorialsByCategoryId"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of tutorials", 
        typeof(IEnumerable<TutorialResource>))]
    public async Task<IActionResult> GetTutorialsByCategoryId(int categoryId)
    {
        var getTutorialsByCategoryQuery = new GetAllTutorialByCategoryIdQuery(categoryId);
        var tutorials = await tutorialQueryService.Handle(getTutorialsByCategoryQuery);
        var tutorialResources = tutorials.Select(
            TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
}