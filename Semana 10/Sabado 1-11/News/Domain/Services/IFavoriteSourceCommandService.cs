using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Commands;

namespace CatchUpPlatform.API.News.Domain.Services;

public interface IFavoriteSourceCommandService
{
    /// <summary>
    ///  Handle the create favorite source command
    /// </summary>
    /// <remarks>
    ///     This method processes the CreateFavoriteSourceCommand to create a new FavoriteSource entity.
    ///     It performs necessary validations and persists the entity to the data store.
    ///     If it does not exist, it creates a new FavoriteSource and returns it; otherwise, it returns null.
    /// </remarks>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<FavoriteSource?> Handle(CreateFavoriteSourceCommand command);
}