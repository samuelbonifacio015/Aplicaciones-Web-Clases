using hello_asp_net_developer.Generic.Domain.Model.Entities;
using hello_asp_net_developer.Generic.Interfases.REST.Resources;

namespace hello_asp_net_developer.Generic.Interfases.REST.Assemblers;

/// <summary>
/// Assembler class for converting between Developer entity and GreetDeveloperResponse.
/// Provides methods to transform domain model entities into response data.
/// </summary>
public static class GreetDeveloperAssembler
{
    /// <summary>
    /// Converts a Developer entity into a GreetDeveloperResponse.
    /// If the developer is null or has any empty names, returns a response with a generic
    /// greeting for an anonymous ASP .NET developer.
    /// </summary>
    /// <param name="developer">The developer entity to convert, may be null</param>
    /// <returns>A GreetDeveloperResponse with main greeting details</returns>
    public static GreetDeveloperResponse ToResponseFromEntity(Developer? developer)
    {
        if (developer == null || developer.IsAnyNameEmpty())
        {
            return new GreetDeveloperResponse(message:"Welcome Anonymous ASP .NET Developer!");
        }
        return new GreetDeveloperResponse(developer.Id, developer.GetFullName(),
            Message: $"Welcome {developer.GetFullName()} ASP .NET Developer!");
    }
}   