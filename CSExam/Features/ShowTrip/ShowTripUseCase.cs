using CSExam.Entites;
using Microsoft.EntityFrameworkCore;

namespace CSExam.Features.ShowTrip;

public class ShowTripUseCase(
    CSExamDbContext ctx
)
{
    public async Task<Result<ShowTripResponse>> Do(ShowTripPayload payload)
    {
        var trip = await ctx.Trips
            .Include(t => t.User)
                .ThenInclude(u => u.TripPoints)
            .FirstOrDefaultAsync(t => t.ID == payload.TripID);

        if (trip is null)
            return Result<ShowTripResponse>.Fail("Trip not found");

        var points = trip.Points.Select(p => new PointTitle(p.Title)).ToList();

        var response = new ShowTripResponse(
            trip.Title,
            trip.Description,
            trip.User.Name
        // trip.Points
        //      .Select(p => new PointTitle(p.Title))
        // .ToList();
        );

        return Result<ShowTripResponse>.Success(response);

    }
}