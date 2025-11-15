namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
/// <summary>
///     Represents a query to retrieve all tutorials by a specific category identifier.
/// </summary>
/// <param name="CategoryId">
///     The unique identifier of the category whose tutorials are to be retrieved.
/// </param>
public record GetAllTutorialByCategoryIdQuery(int CategoryId);