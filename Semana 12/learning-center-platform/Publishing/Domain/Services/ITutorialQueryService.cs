using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
/// <summary>
///  Represents the contract for tutorial query services.
/// </summary>
public interface ITutorialQueryService
{
    /// <summary>
    ///     Handles the retrieval of a tutorial by its unique identifier.
    /// </summary>
    Task<Tutorial?> Handle(GetTutorialByIdQuery query);
    
    /// <summary>
    ///     Handles the retrieval of all tutorials.
    /// </summary>
    Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsQuery query);
    
    /// <summary>
    ///     Handles the retrieval of all tutorials by category identifier.
    /// </summary>
    Task<IEnumerable<Tutorial>> Handle(GetAllTutorialByCategoryIdQuery query);
}