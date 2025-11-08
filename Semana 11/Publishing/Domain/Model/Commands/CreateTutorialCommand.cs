namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
/// <summary>
///     Command to create a new tutorial
/// </summary>
/// <param name="Title">
///     The title of the tutorial to be created
/// </param>
/// <param name="Summary">
///     The summary of the tutorial to be created
/// </param>
/// <param name="CategoryId">
///     The category ID of the tutorial to be created
/// </param>
public record CreateTutorialCommand(string Title,
    string Summary, int CategoryId);