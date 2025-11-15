using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
/// <summary>
///  ///     Represents a readable content asset associated with a content item.
/// </summary>
public class ReadableContentAsset : Asset
{
    public string ReadableContent { get; set; }
    public override bool Readable => true;
    public override bool Viewable => false;

    public ReadableContentAsset() : base(EAssetType.ReadableContentItem)
    {
    }

    public ReadableContentAsset(string content) : base(EAssetType.ReadableContentItem)
    {
        ReadableContent = content;
    }

}