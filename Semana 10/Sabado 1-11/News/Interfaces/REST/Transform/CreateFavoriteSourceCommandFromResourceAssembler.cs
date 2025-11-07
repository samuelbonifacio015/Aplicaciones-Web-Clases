using CatchUpPlatform.API.News.Domain.Model.Commands;
using CatchUpPlatform.API.News.Interfaces.REST.Resources;

namespace CatchUpPlatform.API.News.Interfaces.REST.Transform;

/// <summary>
///     Assembler to transform CreateFavoriteSourceResource to CreateFavoriteSourceCommand.
/// </summary>
public static class CreateFavoriteSourceCommandFromResourceAssembler
{
    public static CreateFavoriteSourceCommand ToCommandFromResource(CreateFavoriteSourceResource resource) =>
    new CreateFavoriteSourceCommand(resource.NewsApikey, resource.SourceId);
}