using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST;
/// <summary>
///     The category controller
/// </summary>
/// <param name="categoryQueryService">
///     The <see cref="ICategoryQueryService"/> category query service
/// </param>
/// <param name="categoryCommandService">
/// ///     The <see cref="ICategoryCommandService"/> category command service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Category Endpoints")]
public class CategoriesController(
    ICategoryQueryService categoryQueryService,
    ICategoryCommandService categoryCommandService) : ControllerBase
{

    /// <summary>
    ///  Get category by id
    /// </summary>
    /// <param name="categoryId">
    ///     The category id
    /// </param>
    /// <returns>
    ///     the <see cref="CategoryResource"/> category
    /// </returns>
    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get Category By Id",
        Description = "Get Category By Id",
        OperationId = "GetCategoryById")]
    [SwaggerResponse(StatusCodes.Status200OK, "the category found", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "the category not found")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        if (category is null) return NotFound();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }

    /// <summary>
    ///   Create a new category
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateCategoryResource"/> create category resource
    /// </param>
    /// <returns>
    ///     The <see cref="CategoryResource"/> category created, or bad request if not created
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new category", 
        Description = "Create a new category",
        OperationId = "CreateCategory")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "the category was created", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "the category could not be created")]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryResource resource)
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();  
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return CreatedAtAction(nameof(GetCategoryById), 
            new { categoryId = category.Id }, categoryResource);
    }
    /// <summary>
    ///     Get all categories
    /// </summary>
    /// <returns>
    ///     The list of <see cref="CategoryResource"/> categories
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Get all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of categories", typeof(IEnumerable<CategoryResource>))]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryQueryService.Handle(new GetAllCategoriesQuery());
        var categoryResources = categories
            .Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(categoryResources);
    }
}
    
