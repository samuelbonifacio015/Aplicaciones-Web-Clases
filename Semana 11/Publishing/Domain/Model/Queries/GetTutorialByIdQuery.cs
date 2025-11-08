namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
/// <summary>
///     Represents a query to retrieve a tutorial by its unique identifier.
/// </summary>
/// <param name="TutorialId">
///     The unique identifier of the tutorial to be retrieved.
/// </param>
public record GetTutorialByIdQuery(int TutorialId);