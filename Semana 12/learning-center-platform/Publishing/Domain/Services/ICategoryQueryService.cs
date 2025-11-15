using ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
/// <summary>
///  Represents the contract for category query services.
/// </summary>
public interface ICategoryQueryService
{
    /// <summary>
    ///  Handles the retrieval of all categories.
    /// </summary>
    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
    
    /// <summary>
    ///  Handles the retrieval of a category by its unique identifier.
    /// </summary>
    Task<Category?> Handle(GetCategoryByIdQuery query);
    
    
}