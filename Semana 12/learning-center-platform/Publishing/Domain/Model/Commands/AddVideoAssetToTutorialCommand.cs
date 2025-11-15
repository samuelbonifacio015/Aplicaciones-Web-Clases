namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
/// <summary>
///     Command to add a video asset to a tutorial
/// </summary>
/// <param name="VideoUrl">
///     The URL of the video asset to be added
/// </param>
/// <param name="TutorialId">
///     The ID of the tutorial to which the video asset will be added
/// </param>
public record AddVideoAssetToTutorialCommand(string VideoUrl,
    int TutorialId);