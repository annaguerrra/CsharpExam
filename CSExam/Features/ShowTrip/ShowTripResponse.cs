using System.Drawing;

namespace CSExam.Features.ShowTrip;

public record ShowTripResponse(
    string Title,
    string Description,
    string Username
    // List<Point> Points
);

public record PointTitle(
    string Name
);