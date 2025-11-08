using ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ACME.LearningCenterPlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;
/// <summary>
///     Repository implementation for Category entity.
/// </summary>
/// <param name="context"></param>
public class CategoryRepository(AppDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository;