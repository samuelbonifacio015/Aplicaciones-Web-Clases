using System.ComponentModel.DataAnnotations;

namespace CatchUpPlatform.API.News.Interfaces.REST.Resources;
/// <summary>
///     Represents the resource to create a favorite news source.
/// </summary>
public record CreateFavoriteSourceResource(
    [Required] string NewsApikey,
    [Required] string SourceId);