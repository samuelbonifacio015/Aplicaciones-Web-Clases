using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Queries;

namespace CatchUpPlatform.API.News.Domain.Services;
/// <summary>
///     Interface for Favorite Source Query Services
/// </summary>
/// <remarks>
///     This interface defines the contract for query services related to FavoriteSource entities.
/// </remarks>
public interface IFavoriteSourceQueryServices
{
    /// <summary>
    ///     Handle the GetAllFavoriteSourcesByNewsApikeyQuery
    /// </summary>
    /// <remarks>
    ///     This method handles the GetAllFavoriteSourcesByNewsApikeyQuery to retrieve all
    ///     FavoriteSource entities associated with a specific News API key.
    /// </remarks>
    /// <param name="query">The GetAllFavoriteSourcesByNewsApikeyQuery query</param>
    /// <returns>An IEnumerable containing the FavoriteSource objects</returns>
    Task<IEnumerable<FavoriteSource>> Handle(GetAllFavoriteSourcesByNewsApikeyQuery query);
    
    /// <summary>
    ///     Handle the GetFavoriteSourceByNewsApikeyAndSourceIdQuery
    /// </summary>
    /// <remarks>
    ///     This method handles the GetFavoriteSourceByNewsApikeyAndSourceIdQuery to retrieve a
    ///     FavoriteSource entity based on the provided News API key and Source ID.
    /// </remarks>
    /// <param name="query">The GetFavoriteSourceByNewsApikeyAndSourceIdQuery query</param>
    /// <returns>The FavoriteSource object if found, or null otherwise</returns>
    Task<FavoriteSource?> Handle(GetFavoriteSourceByNewsApikeyAndSourceIdQuery query);
    
    /// <summary>
    ///     Handle the GetFavoriteSourceByIdQuery 
    /// </summary>
    /// <remarks>
    ///     This method handles the GetFavoriteSourceByIdQuery to retrieve a
    ///     FavoriteSource entity by its unique identifier.
    /// </remarks>
    /// <param name="query">The GetFavoriteSourceByIdQuery query</param>
    /// <returns>
    ///     The FavoriteSource if found; otherwise, null
    /// </returns>
    Task<FavoriteSource?> Handle(GetFavoriteSourceByIdQuery query);
    
}