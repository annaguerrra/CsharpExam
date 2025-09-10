namespace CSExam.Features.EditTrip;

public record EditTripPayload(
    Guid UserID,
    Guid TripID,
    string Title
);