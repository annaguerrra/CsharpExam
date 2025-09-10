using System.Security.Claims;
using CSExam.Features.CreateTrip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CSExam.Endpoints;

public static class TripEndPoints
{
    public static void ConfigureTripEndpoints(this WebApplication app)
    {
        app.MapPost("/newtrip", async (
            HttpContext http,
            [FromBody] CreateTripPayload payload,
            string id,
            [FromServices] CreateTripUseCase useCase
        ) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return Results.BadRequest("User not found");

            var userId = Guid.Parse(id);

            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };
        });
        
    }
}