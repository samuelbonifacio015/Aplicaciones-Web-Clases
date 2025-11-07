namespace CatchUpPlatform.API.News.Domain.Model.Queries;

/// <summary>
///     Query to get a FavoriteSource aggregate by NewsApikey and SourceId
/// </summary>
/// <param name="NewsApikey">The NewsApikey to search</param>
/// <param name="SourceId">The Source ID to search</param>
public record GetFavoriteSourceByNewsApikeyAndSourceIdQuery(string NewsApikey, string SourceId);