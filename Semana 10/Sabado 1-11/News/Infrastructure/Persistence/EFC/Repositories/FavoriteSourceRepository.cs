using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatchUpPlatform.API.News.Infrastructure.Persistence.EFC.Repositories;
/// <summary>
///     Favorite Source Repository Implementation
/// </summary>
/// <remarks>
///     This class implements the repository pattern for managing FavoriteSource entities using Entity Framework Core.
///     It provides methods to perform CRUD operations and custom queries specific to FavoriteSource.
/// </remarks>
/// <param name="context"></param>
public class FavoriteSourceRepository(AppDbContext context)
    : BaseRepository<FavoriteSource>(context), IFavoriteSourceRepository
{
    public async Task<IEnumerable<FavoriteSource>> FindByNewsApikeyAsync(string newsApiKey)
    {
        return await Context.Set<FavoriteSource>().Where( f => f.NewsApikey == newsApiKey).ToListAsync();
    }

    public async Task<FavoriteSource?> FindByNewsApikeyAndSourceIdAsync(string newsApiKey, string sourceId)
    {
        return await Context.Set<FavoriteSource>()
            .FirstOrDefaultAsync(f => f.NewsApikey == newsApiKey && f.SourceId == sourceId);
    }
}