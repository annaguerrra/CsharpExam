using Microsoft.Identity.Client;

namespace CSExam.Entites;

public class TripPoints
{
    public Guid ID { get; set; }

    public Guid UserID { get; set; }
    public User User { get; set; }

    public Guid TripID { get; set; }
    public Trip Trip { get; set; }

    public Guid PointID { get; set; }
    public Point Point { get; set; }
    
}