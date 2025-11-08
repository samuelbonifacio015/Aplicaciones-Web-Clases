using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable
{
    public ICollection<Asset> Assets { get; }
    public EPublishingStatus Status { get; protected set; }
    public bool Readable => HasReadableAssets;
    public bool Viewable => HasViewableAssets;
    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);
    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);

    public Tutorial()
    {
        Title = string.Empty;
        Summary = string.Empty;
        Assets = new List<Asset>();
        Status = EPublishingStatus.Draft;
    }
    /// <summary>
    ///  Verify if all assets have the specified status
    /// </summary>
    /// <param name="status">
    ///     The status to verify
    /// </param>
    /// <returns>
    ///     True if all assets have the specified status, false otherwise
    /// </returns>
    private bool HasAllAssetsWithStatus(EPublishingStatus status)
    {
        return Assets.All(asset => asset.Status == status);
    }
    /// <summary>
    ///     Verify if a readable content asset with the specified content exists
    /// </summary>
    /// <param name="content">
    ///   The content to verify
    /// </param>
    /// <returns>
    ///     True if a readable content asset with the specified content exists, false otherwise
    /// </returns>
    private bool ExistsReadableContent(string content)
    {
        return Assets.Any(asset => asset.Type == EAssetType.ReadableContentItem &&
                                   (string)asset.GetContent() == content);
    }
    
    /// <summary>
    ///     Verify if a video asset with the specified content exists
    /// </summary>
    /// <param name="content">
    ///   The content to verify
    /// </param>
    /// <returns>
    ///     True if a video asset with the specified content exists, false otherwise
    /// </returns>
    private bool ExistsVideoByUrl(string videoUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Video &&
                                   (string)asset.GetContent() == videoUrl);
    }
    
    /// <summary>
    ///     Verify if an image asset with the specified content exists
    /// </summary>
    /// <param name="content">
    ///   The content to verify
    /// </param>
    /// <returns>
    ///     True if an image asset with the specified content exists, false otherwise
    /// </returns>
    private bool ExistsImageByUrl(string imageUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image &&
                                   (string)asset.GetContent() == imageUrl);
    }

    /// <summary>
    ///     Add an image asset to the tutorial
    /// </summary>
    /// <param name="imageUrl">
    ///     The URL of the image to add
    /// </param>
    public void AddImage(string imageUrl)
    {
        if (ExistsImageByUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }
    
    /// <summary>
    ///     Add a video asset to the tutorial
    /// </summary>
    /// <param name="videoUrl">
    ///     The URL of the video to add
    /// </param>
    public void AddVideo(string videoUrl)
    {
        if (ExistsVideoByUrl(videoUrl)) return;
        Assets.Add(new VideoAsset(videoUrl));
    }
    
    /// <summary>
    ///     Add a readable content asset to the tutorial
    /// </summary>
    /// <param name="content">
    ///     The content to add
    /// </param>
    public void AddReadableContent(string content)
    {
        if (ExistsReadableContent(content)) return;
        Assets.Add(new ReadableContentAsset(content));
    }

    /// <summary>
    ///  Remove an asset from the tutorial by its identifier
    /// </summary>
    /// <param name="identifier">
    ///     The identifier of the asset to remove
    /// </param>
    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(asset => asset.AssetIdentifier == identifier);
        if (asset is null) return;  
        Assets.Remove(asset);
    }
    /// <summary>
    ///  Clear all assets from the tutorial
    /// </summary>
    public void ClearAssets()
    {
        Assets.Clear();
    }

    /// <summary>
    ///  Build and return the content of the tutorial
    /// </summary>
    /// <remarks>
    ///     This method is used to build the content of the tutorial for publishing.
    /// </remarks>
    /// <returns></returns>
    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        if (Assets.Count > 0)
            content.AddRange(Assets.Select(
                asset => new ContentItem(asset.Type.ToString(), asset.GetContent() as string ?? string.Empty)));
        return content;
    }

    public void SendToEdit()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }
    /// <summary>
    ///  Send to approval the tutorial    
    /// </summary>
    public void SendToApproval()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }
    /// <summary>
    ///  Approve and lock the tutorial for publishing
    /// </summary>
    public void ApprovedAndLock()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ApprovedAndLocked))
            Status = EPublishingStatus.ApprovedAndLocked;
    }

    /// <summary>
    ///  Reject the tutorial and return it to the "Draft" status
    /// </summary>
    public void Reject()
    {
        Status = EPublishingStatus.Draft;
    }

    /// <summary>
    ///   Return the tutorial to the "Ready to Edit" status
    /// </summary>
    public void ReturnToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }
}