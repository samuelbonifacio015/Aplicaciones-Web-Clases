using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
/// <summary>
///     Represents a video asset associated with a content item.
/// </summary>
public class VideoAsset : Asset
{
    public Uri? VideoUri { get; private set; }
    public override bool Readable => false;
    public override bool Viewable => true;

    public VideoAsset() : base(EAssetType.Video)
    {
    }

    public VideoAsset(string videoUrl) : base(EAssetType.Video)
    {
        VideoUri = new Uri(videoUrl);
    }
    
}