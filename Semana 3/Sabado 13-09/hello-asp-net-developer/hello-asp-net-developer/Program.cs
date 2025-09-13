using hello_asp_net_developer.Generic.Domain.Model.Entities;
using hello_asp_net_developer.Generic.Interfases.REST.Assemblers;
using hello_asp_net_developer.Generic.Interfases.REST.Assemblers;
using hello_asp_net_developer.Generic.Interfases.REST.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

/// <summary>
/// Defines the POST endpoint for creating a greeting for a developer.
/// </summary>
/// <param name="request">The request payload containing developer details.</param>
/// <returns>A InActionResult containing a GreetDeveloperResponse with a 201 Created status.</returns>
app.MapPost("/greetings", (GreetDeveloperRequest request) =>
    {
        var developer = DeveloperAssembler.ToEntityFromRequest(request);
        var response = GreetDeveloperAssembler.ToResponseFromEntity(developer);
        return Results.Created("/greetings", response);
    })
    .WithName("CreateGreeting")
    .WithOpenApi();

/// <summary>
/// Defines the GET endpoint for retrieving a greeting for a developer.
/// </summary>
/// <param name="firstName">The first name of the developer, optional.</param>
/// <param name="lastName">The last name of the developer, optional.</param>
/// <returns>A InActionResult containing a GreetDeveloperResponse with a 200 OK status.</returns>
app.MapGet("/greetings", (string? firstName, string? lastName) =>
    {
        var developer = !string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName)
            ? new Developer(firstName, lastName)
            : null;
        var response = GreetDeveloperAssembler.ToResponseFromEntity(developer);
        return Results.Ok(response);
    })
    .WithName("GetGreetings")
    .WithOpenApi();
app.Run();