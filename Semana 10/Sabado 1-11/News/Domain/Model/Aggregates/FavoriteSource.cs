using CatchUpPlatform.API.News.Domain.Model.Commands;

namespace CatchUpPlatform.API.News.Domain.Model.Aggregates;
/// FavoriteSource Aggregate
/// <summary>
///     This class represents the FavoriteSource aggregate in the News domain.
/// </summary>
public partial class FavoriteSource
{
  public int Id { get; }
  public string NewsApikey { get; private set; }
  public string SourceId { get; private set; }

  protected FavoriteSource()
  {
    NewsApikey = string.Empty;
    SourceId = string.Empty;
  }
  
  /// <summary>
  ///   Constructor for FavoriteSource aggregate
  /// </summary>
  /// <remarks>
  ///   The constructor initializes a new instance of the FavoriteSource class with the specified news API key and source ID.
  /// </remarks>
  /// <param name="newsApikey"></param>
  /// <param name="sourceId"></param>
  public FavoriteSource(CreateFavoriteSourceCommand command)
  {
    NewsApikey = command.NewsApikey;
    SourceId = command.SourceId;
  }
}