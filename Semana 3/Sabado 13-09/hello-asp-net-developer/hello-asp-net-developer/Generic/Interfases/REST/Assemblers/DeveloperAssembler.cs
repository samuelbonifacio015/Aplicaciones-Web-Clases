using hello_asp_net_developer.Generic.Domain.Model.Entities;
using hello_asp_net_developer.Generic.Interfases.REST.Resources;

namespace hello_asp_net_developer.Generic.Interfases.REST.Assemblers;

/// <summary>
/// Assembler class for converting between GreetDeveloperRequest and Developer entity.
/// Provides methods to transform request data into domain model entities.
/// </summary>
public static class DeveloperAssembler
{
    /// <summary>
    /// Assembler class for converting between GreetDeveloperRequest and Developer entity.
    /// Provides methods to transform request data into domain model entities.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static Developer? ToEntityFromRequest(GreetDeveloperRequest? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.FirstName)
                            || string.IsNullOrWhiteSpace(request.LastName))
        {
            return null;
        }
        return new Developer(request.FirstName, request.LastName);
    }
}