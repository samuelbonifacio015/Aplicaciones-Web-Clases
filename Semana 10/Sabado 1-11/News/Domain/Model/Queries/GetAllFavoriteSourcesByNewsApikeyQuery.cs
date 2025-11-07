namespace CatchUpPlatform.API.News.Domain.Model.Queries;

/// <summary>
///     Query to get all FavoriteSource aggregates by NewsApikey
/// </summary>
/// <param name="NewsApikey">The NewsApikey to search</param>
public record GetAllFavoriteSourcesByNewsApikeyQuery(string NewsApikey);