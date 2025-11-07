using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Interfaces.REST.Resources;

namespace CatchUpPlatform.API.News.Interfaces.REST.Transform;

/// <summary>
///     Assembler to transform FavoriteSource entity to FavoriteSourceResource.
/// </summary>
public static class FavoriteSourceResourceFromEntityAssembler
{
    public static FavoriteSourceResource ToResourceFromEntity(FavoriteSource entity) =>
        new FavoriteSourceResource(entity.Id, entity.NewsApikey, entity.SourceId);
}