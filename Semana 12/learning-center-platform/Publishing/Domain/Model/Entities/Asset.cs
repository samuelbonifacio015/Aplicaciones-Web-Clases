using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
/// <summary>
///     Represents a digital asset associated with a content item.
/// </summary>
/// <param name="type">
///     The type of the asset.
/// </param>
public partial class Asset(EAssetType type): IPublishable
{
    public int Id { get; }

    public AcmeAssetIdentifier AssetIdentifier { get; private set; } = new();
    public EPublishingStatus Status { get; private set; } = EPublishingStatus.Draft;
    
    public EAssetType Type { get; private set; } = type;

    public virtual bool Readable => false;
    
    public virtual bool Viewable => false;
    
    /// <summary>
    ///     Approves the asset and transitions its status to ReadyToEdit.
    /// </summary>
    public void SendToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }

    /// <summary>
    ///     Sends the asset for approval, transitioning its status to ReadyToApproval.
    ///    </summary> 
    public void SendToApproval()
    {
        Status = EPublishingStatus.ReadyToApproval;
    }

    /// <summary>
    ///   Approves the asset and locks it, transitioning its status to ApprovedAndLocked.
    /// </summary>
    public void ApprovedAndLock()
    {
        Status = EPublishingStatus.ApprovedAndLocked;
    }

    /// <summary>
    ///  Rejects the asset, transitioning its status back to Draft.
    /// </summary>
    public void Reject()
    {
        Status = EPublishingStatus.Draft;
    }

    /// <summary>
    ///  Returns the asset to edit mode, transitioning its status to ReadyToEdit.
    /// </summary>
    public void ReturnToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }
    /// <summary>
    ///     Gets the content of the asset.
    /// </summary>
    /// <returns>
    ///     The content of the asset.
    /// </returns>
    public virtual object GetContent()
    {
        return string.Empty;
    }
}