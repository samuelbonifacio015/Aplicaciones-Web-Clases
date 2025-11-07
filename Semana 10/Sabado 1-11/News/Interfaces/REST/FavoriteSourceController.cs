using System.Net.Mime;
using CatchUpPlatform.API.News.Domain.Model.Queries;
using CatchUpPlatform.API.News.Domain.Services;
using CatchUpPlatform.API.News.Interfaces.REST.Resources;
using CatchUpPlatform.API.News.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CatchUpPlatform.API.News.Interfaces.REST;
/// <summary>
///     FavoriteSource REST API controller.
/// </summary>
/// <param name="favoriteSourceCommandService">The Favorite Source Command Service</param>
/// <param name="favoriteSourceQueryServices">The Favorite Source Query Service</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("FavoriteSource")]
public class FavoriteSourceController(
    IFavoriteSourceCommandService favoriteSourceCommandService,
    IFavoriteSourceQueryServices favoriteSourceQueryServices)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new Favorite Source", Description = "Create a new Favorite Source", OperationId = "CreateFavoriteSource")]
    [SwaggerResponse(201, "Created favorite Source", typeof(FavoriteSourceResource))]
    [SwaggerResponse(400, "The favorite Source was not created")]
    [SwaggerResponse(409, "The favorite Source already exists")]
    public async Task<IActionResult> CreateFavoriteSource(
        [FromBody] CreateFavoriteSourceResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createFavoriteSourceCommand = CreateFavoriteSourceCommandFromResourceAssembler.ToCommandFromResource(resource);
        try
        {
            var result = await favoriteSourceCommandService.Handle(createFavoriteSourceCommand);
            if (result is null) return BadRequest();
            return CreatedAtAction(nameof(GetFavoriteSourceById), new { id = result.Id },
                FavoriteSourceResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception e) when (e.Message.Contains("already exists"))
        {
            return Conflict("A favorite source with the same NewsApikey and SourceId already exists.");
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets a Favorite Source by Id", Description = "Gets a Favorite Source for a given identifier", OperationId = "GetFavoriteSourceById")]
    [SwaggerResponse(200, "The favorite Source was found", typeof(FavoriteSourceResource))]
    public async Task<IActionResult> GetFavoriteSourceById(int id)
    {
        var getFavoriteSourceByIdQuery = new GetFavoriteSourceByIdQuery(id);
        var result = await favoriteSourceQueryServices.Handle(getFavoriteSourceByIdQuery);
        if (result is null) return NotFound();
        var resource = FavoriteSourceResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFavoriteSourceFromQuery([FromQuery] string newsApikey, [FromQuery] string sourceId="")
    {
        return string.IsNullOrEmpty(sourceId)
            ? await GetAllFavoriteSourcesByNewsApikey(newsApikey)
            : await GetFavoriteSourceByNewsApikeyAndSourceId(newsApikey, sourceId);
    }
    
    private async Task<IActionResult> GetAllFavoriteSourcesByNewsApikey(string newsApikey)
    {
        var getAllFavoriteSourcesByNewsApikeyQuery = new GetAllFavoriteSourcesByNewsApikeyQuery(newsApikey);
        var results = await favoriteSourceQueryServices.Handle(getAllFavoriteSourcesByNewsApikeyQuery);
        var resources = results
            .Select(FavoriteSourceResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        return Ok(resources);
    }
    
    private async Task<IActionResult> GetFavoriteSourceByNewsApikeyAndSourceId(string newsApikey, string sourceId)
    {
        var getFavoriteSourceByNewsApikeyAndSourceIdQuery = new GetFavoriteSourceByNewsApikeyAndSourceIdQuery(newsApikey, sourceId);
        var result = await favoriteSourceQueryServices.Handle(getFavoriteSourceByNewsApikeyAndSourceIdQuery);
        if (result is null) return NotFound();
        var resource = FavoriteSourceResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}