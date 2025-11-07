using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Queries;
using CatchUpPlatform.API.News.Domain.Repositories;
using CatchUpPlatform.API.News.Domain.Services;

namespace CatchUpPlatform.API.News.Application.Internal.QueryServices;
/// <summary>
///     Favorite Source Query Service
/// </summary>
/// <remarks>
///     This class handles queries related to favorite news sources.
///     It interacts with the IFavoriteSourceRepository to retrieve data.
/// </remarks>
/// <param name="favoriteSourceRepository"></param>
public class FavoriteSourceQueryService(IFavoriteSourceRepository favoriteSourceRepository)
    : IFavoriteSourceQueryServices
{
    public async Task<IEnumerable<FavoriteSource>> Handle(GetAllFavoriteSourcesByNewsApikeyQuery query)
    {
        return await favoriteSourceRepository.FindByNewsApikeyAsync(query.NewsApikey);
    }

    public async Task<FavoriteSource?> Handle(GetFavoriteSourceByNewsApikeyAndSourceIdQuery query)
    {
        return await favoriteSourceRepository.FindByNewsApikeyAndSourceIdAsync(query.NewsApikey, query.SourceId);
    }

    public async Task<FavoriteSource?> Handle(GetFavoriteSourceByIdQuery query)
    {
        return await favoriteSourceRepository.FindByIdAsync(query.Id);
    }
}