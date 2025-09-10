namespace CSExam.Features.EditTrip;

public record EditTripPayload(
    Guid UserID,
    Guid TripID,
    Guid PointID,
    string Title
);