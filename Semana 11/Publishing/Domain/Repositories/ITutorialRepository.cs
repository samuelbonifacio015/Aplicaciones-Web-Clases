using ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Repositories;

/// <summary>
///    Repository interface for managing Tutorial entities.
/// </summary>
public interface ITutorialRepository : IBaseRepository<Tutorial>
{
    /// <summary>
    ///     Find tutorials by category ID.
    /// </summary>
    Task<IEnumerable<Tutorial>> FindByCategoryIdAsync(int categoryId);
    
    /// <summary>
    ///     Verify if a tutorial exists by its title.
    /// </summary>
    Task<bool> ExistsByTitleAsync(string title);
}