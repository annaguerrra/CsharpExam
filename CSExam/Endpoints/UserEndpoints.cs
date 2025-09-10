using CSExam.Features.Login;
using Microsoft.AspNetCore.Mvc;

namespace CSExam.Endpoints;

public static class UserEndPoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (
            [FromBody] LoginPayload payload,
            [FromServices] LoginUseCase useCase
        ) =>
        {
            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok()
            };
        });
        
    }
}