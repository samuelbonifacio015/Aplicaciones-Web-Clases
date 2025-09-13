namespace hello_asp_net_developer.Generic.Domain.Model.Entities;

/// <summary>
/// Represents a developer with a unique identifier, first name, and last name
/// and trimmed first and last names.
/// </summary>
public class Developer
{
    /// <summary>
    /// Gets the unique identifier for the developer.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
    
    /// <summary>
    /// Gets the first name of the developer, trimmed of leading and trailing whitespace.
    /// </summary>
    public string FirstName { get; }
    
    /// <summary>
    /// Gets the last name of the developer, trimmed of leading and trailing whitespace.
    /// </summary>
    public string LastName { get; }
    
    /// <summary>
    /// Initializes a new instance of the Developer class with the provided first and last names.
    /// </summary>
    /// <param name="firstName">The developer's first name, may be null or contain whitespace</param>
    /// <param name="lastName">The developer's last name, may be null or contain whitespace</param>
    public Developer(string firstName, string lastName)
    {
        FirstName = string.IsNullOrWhiteSpace(firstName) ? "" : firstName.Trim();
        LastName = string.IsNullOrWhiteSpace(lastName) ? "" : lastName.Trim();
    }
    
    /// <summary>
    /// Returns the full name of the developer by concatenating first and last names.
    /// </summary>
    /// <returns>The full name as a trimmed string.</returns>
    public string GetFullName() => $"{FirstName} {LastName}".Trim();
    
    /// <summary>
    /// Checks if either the first name or last name is empty or consists only of whitespace.
    /// </summary>
    /// <returns>True if any name is empty, false otherwise</returns>
    public bool IsAnyNameEmpty() => string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName);
}