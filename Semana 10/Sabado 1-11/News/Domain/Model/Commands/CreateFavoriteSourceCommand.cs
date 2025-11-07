namespace CatchUpPlatform.API.News.Domain.Model.Commands;

/// <summary>
///     Comand to create a FavoriteSource aggregate
/// </summary>
/// <param name="NewsApikey">The NewsApikey obtained from provider</param>
/// <param name="SourceId">The source id of the new source</param>
public record CreateFavoriteSourceCommand(string NewsApikey, string SourceId);