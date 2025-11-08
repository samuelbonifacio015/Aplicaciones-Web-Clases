using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
/// <summary>
///     Represents a category for organizing learning assets.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    /// <summary>
    ///     Constructor to initialize a Category with a specified name.
    /// </summary>
    /// <param name="name">
    ///    The name of the category.
    /// </param>
    public Category(string name)
    {
        Name = name;
    }

    public Category(CreateCategoryCommand command)
    {
        Name = command.Name;
    }

    /// <summary>
    ///    Default constructor initializing Name to an empty string.
    /// </summary>
    public Category()
    {
        Name = string.Empty;
    }
}