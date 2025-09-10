using System.Security.Claims;
using CSExam.Features.CreateTrip;
using CSExam.Features.EditTrip;
using CSExam.Features.ShowTrip;
using Microsoft.AspNetCore.Components.Web;
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

            var request = new CreateTripPayload(userId, payload.Title, payload.Description);
            var result = await useCase.Do(request);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };
        });

        app.MapPut("editTrip", async(
            HttpContext http,
            string iduser,
            string idtrip,
            string idpoint,
            [FromBody] EditTripPayload payload,
            [FromServices] EditTripUseCase useCase
        ) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return Results.BadRequest("User not found");

            var userId = Guid.Parse(iduser);
            var tripId = Guid.Parse(idtrip);
            var pointId = Guid.Parse(idpoint);

            var request = new EditTripPayload(userId, tripId, pointId, payload.Title);
            var result = await useCase.Do(request);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };

        });
        app.MapGet("/showlist", async(
            string Id,
            HttpContext http,
            [FromBody] ShowTripPayload payload, 
            [FromServices] ShowTripUseCase useCase
        ) =>
        {
            var tripId = Guid.Parse(Id);

            var request = new ShowTripPayload(tripId);
            var result = await useCase.Do(request);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };

        });
    }
}