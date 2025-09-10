using CSExam.Entites;

namespace CSExam.Features.CreateTrip;

public class CreateTripUseCase(
    CSExamDbContext ctx
)
{
    public async Task<Result<CreateTripResponse>> Do(CreateTripPayload payload)
    {
        var user = await ctx.Users.FindAsync(payload.UserID);

        if (user is null)
            return Result<CreateTripResponse>.Fail("User not found");

        var trip = new Trip
        {
            Title = payload.Title,
            Description = payload.Description
        };

        user.Trips.Add(trip);
        await ctx.SaveChangesAsync();

        return Result<CreateTripResponse>.Success( new CreateTripResponse(
            payload.Title,
            payload.Description
        ));
    }
}