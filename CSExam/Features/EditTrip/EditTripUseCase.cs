using CSExam.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CSExam.Features.EditTrip;

public class EditTripUseCase(
    CSExamDbContext ctx
)
{
    public async Task<Result<EditTripResponse>> Do(EditTripPayload payload)
    {
        var trip = await ctx.Trips
            .Include(t => t.User)
            .FirstOrDefaultAsync(u => u.UserID == payload.UserID && u.ID == payload.TripID);

        if (trip is null)
            return Result<EditTripResponse>.Fail("Trip not found");

        if(trip.UserID != payload.UserID)
            return Result<EditTripResponse>.Fail("You don't have permission");

        var point = new Point
        {
            Title = payload.Title
        };

        trip.Points.Add(point);
        await ctx.SaveChangesAsync();

        return Result<EditTripResponse>.Success(new EditTripResponse(
            point.Title
        ));
    }
}