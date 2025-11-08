using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
/// <summary>
///     Represents the contract for tutorial command operations.
/// </summary>
public interface ITutorialCommandService
{
    /// <summary>
    ///     Handles the creation of a new tutorial.
    /// </summary>
    Task<Tutorial?> Handle(CreateTutorialCommand command);
    
    /// <summary>
    ///     Handles adding a video asset to an existing tutorial.
    /// </summary>
    Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command);
}