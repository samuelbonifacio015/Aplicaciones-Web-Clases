namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
/// <summary>
///     Represents the unique identifier for an Acme Asset.
/// </summary>
/// <param name="Identifier">
///     The unique identifier value.
/// </param>
public record AcmeAssetIdentifier(Guid Identifier)
{
    /// <summary>
    ///     Default constructor that generates a new unique identifier.         
    /// </summary>
    public AcmeAssetIdentifier() : this(Guid.NewGuid())
    {
    }
}