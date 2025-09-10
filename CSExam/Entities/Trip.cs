namespace CSExam.Entites;

public class Trip
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid UserID { get; set; }
    public User User { get; set; }

    public ICollection<Point> Points { get; set; } = new List<Point>();
    public ICollection<TripPoints> TripPoints { get; set; }

}