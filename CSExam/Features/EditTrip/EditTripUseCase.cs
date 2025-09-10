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
            .Include(t => t.TripPoints)
            .Where(t => t.UserID == payload.UserID)
            .FirstOrDefaultAsync();

        var point = await ctx.TripPoints.FindAsync(payload.PointID);

        if (trip is null)
            return Result<EditTripResponse>.Fail("Trip not found");

        if(trip.UserID != payload.UserID)
            return Result<EditTripResponse>.Fail("You don't have permission");

        if(point is null)
            return Result<EditTripResponse>.Fail("Point not found");

        trip.TripPoints.Add(point);
        
        await ctx.SaveChangesAsync();

        return Result<EditTripResponse>.Success(new EditTripResponse(
            point.Point.Title
        ));
    }
}