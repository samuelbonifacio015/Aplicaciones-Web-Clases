using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace CatchUpPlatform.API.News.Domain.Repositories;
/// <summary>
///     The Favorite Source Repository interface
/// </summary>
public interface IFavoriteSourceRepository : IBaseRepository<FavoriteSource>
{
    /// <summary>
    ///     Find Favorite Sources by News API Key
    /// </summary>
    /// <param name="newsApiKey">The news API Key</param>
    /// <returns>
    ///     An Enumerable of FavoriteSource if found; otherwise, an empty Enumerable
    /// </returns>
    Task<IEnumerable<FavoriteSource>> FindByNewsApikeyAsync(string newsApiKey);
    
    /// <summary>
    ///     Find Favorite Source by News API Key and Source ID
    /// </summary>
    /// <param name="newsApiKey">The news API key</param>
    /// <param name="sourceId">The Source ID</param>
    /// <returns>
    ///     The favorite source if found; otherwise, null
    /// </returns>
    Task<FavoriteSource?> FindByNewsApikeyAndSourceIdAsync(string newsApiKey, string sourceId);
}