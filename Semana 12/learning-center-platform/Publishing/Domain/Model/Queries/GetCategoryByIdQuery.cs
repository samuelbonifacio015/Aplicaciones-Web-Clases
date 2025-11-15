namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
/// <summary>
///  Represents a query to retrieve a category by its unique identifier.
/// </summary>
/// <param name="CategoryId">
///     The unique identifier of the category to be retrieved.
/// </param>
public record GetCategoryByIdQuery(int CategoryId);