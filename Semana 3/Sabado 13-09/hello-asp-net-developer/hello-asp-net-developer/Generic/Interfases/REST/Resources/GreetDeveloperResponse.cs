namespace hello_asp_net_developer.Generic.Interfases.REST.Resources;

/// <summary>
/// A record representing a response to a greet developer request.
/// Containing the developer's ID, full name, and a greeting message, used as output for GET and POST responses.
/// </summary>
/// <param name="id">The unique identifier of the developer, may be null for anonymous responses</param>
/// <param name="FullName">The full name of the developer, may be null for anonymous responses</param>
/// <param name="Message">The greeting message</param>
public record GreetDeveloperResponse(Guid? id, string? FullName, string Message)
{
    /// <summary>
    /// Constructs a response with only a message, used for anonymous greetings.
    /// </summary>
    /// <param name="message">The greetings messae, typically for anonymous cases</param>

    public GreetDeveloperResponse(string message) : this(null, null, message) { }
}