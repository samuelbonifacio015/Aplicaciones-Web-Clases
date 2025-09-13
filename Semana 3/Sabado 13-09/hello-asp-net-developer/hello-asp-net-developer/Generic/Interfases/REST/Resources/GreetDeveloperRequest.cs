namespace hello_asp_net_developer.Generic.Interfases.REST.Resources;

/// <summary>
/// A Record representing a request to greet a developer, containing optional first and last names.
/// </summary>
/// <param name="FirstName">The developer's first name, may be null</param>
/// <param name="LastName">The developer's last name, may be null</param>
public record GreetDeveloperRequest(string? FirstName, string? LastName);