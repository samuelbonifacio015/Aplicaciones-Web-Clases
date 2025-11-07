using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace CatchUpPlatform.API.News.Domain.Model.Aggregates;

/// <summary>
///     FavoriteSource Aggregate with Created and Updated Date properties
/// </summary>
/// <remarks>
///     This class represents the FavoriteSource aggregate with additional properties for CreatedDate and UpdatedDate.
///     It implements the IEntityWithCreatedUpdatedDate interface to ensure consistency in tracking creation and modification
/// </remarks>
public partial class FavoriteSource : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}