namespace CSExam.Entites;

public class User
{
    public Guid Name { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public ICollection<Trip> Trips { get; set; }
    public ICollection<Point> Points { get; set; }
    public ICollection<TripPoints> TripPoints { get; set; }

}