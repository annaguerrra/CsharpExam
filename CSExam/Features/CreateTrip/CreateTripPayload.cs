
namespace CSExam.Features.CreateTrip;

public record CreateTripPayload(
    Guid UserID,
    string Title,
    string Description
);