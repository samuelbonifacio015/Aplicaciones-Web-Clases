namespace CatchUpPlatform.API.News.Interfaces.REST.Resources;
/// <summary>
///     Represents the resource for a favorite news source.
/// </summary>
public record FavoriteSourceResource(int Id, string NewsApikey, string SourceId);