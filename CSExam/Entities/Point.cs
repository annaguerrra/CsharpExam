namespace CSExam.Entites;

public class Point
{
    public Guid ID { get; set; }
    public string Title { get; set; }

    public Guid UserID { get; set; }
    public User User { get; set; }

    public ICollection<TripPoints> TripPoints { get; set; }
    
}