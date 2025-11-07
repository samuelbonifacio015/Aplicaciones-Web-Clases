using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Commands;
using CatchUpPlatform.API.News.Domain.Repositories;
using CatchUpPlatform.API.News.Domain.Services;
using CatchUpPlatform.API.Shared.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace CatchUpPlatform.API.News.Application.Internal.CommandServices;

/// <summary>
/// This class handles commands related to FavoriteSource entities.
/// </summary>
/// <param name="favoriteSourceRepository">The instance of FavoriteSourceRepository</param>
/// <param name="unitOfWork">The instance of UnitOfwork</param>
public class FavoriteSourceCommandService(IFavoriteSourceRepository favoriteSourceRepository,
                                            IUnitOfWork unitOfWork) 
    : IFavoriteSourceCommandService 
{
    public async Task<FavoriteSource?> Handle(CreateFavoriteSourceCommand command)
    {
        var favoriteSource =
            await favoriteSourceRepository.FindByNewsApikeyAndSourceIdAsync(command.NewsApikey, command.SourceId);
        if (favoriteSource != null)
            throw new Exception("Favorite source already exists.");
        favoriteSource = new FavoriteSource(command);
        try
        {
            await favoriteSourceRepository.AddAsync(favoriteSource);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
        return favoriteSource;
    }
}