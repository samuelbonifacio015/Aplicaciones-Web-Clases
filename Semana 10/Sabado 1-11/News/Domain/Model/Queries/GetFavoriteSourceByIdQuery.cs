namespace CatchUpPlatform.API.News.Domain.Model.Queries;
/// <summary>
///     Query to get a FavoriteSource aggregate by Id
/// </summary>
/// <param name="Id">The Source Id to search</param>
public record GetFavoriteSourceByIdQuery(int Id);