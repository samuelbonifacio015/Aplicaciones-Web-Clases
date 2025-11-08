using ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
/// <summary>
///  Represents the contract for category command operations.
/// </summary>
public interface ICategoryCommandService
{
    /// <summary>
    ///     Handles the creation of a new category.
    /// </summary>
    Task<Category?> Handle(CreateCategoryCommand command);
}