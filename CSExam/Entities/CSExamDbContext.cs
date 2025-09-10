using Microsoft.EntityFrameworkCore;

namespace CSExam.Entites;

public class CSExamDbContext(DbContextOptions opt) : DbContext(opt)
{
    DbSet<User> Users => Set<User>();
    DbSet<Trip> Trips => Set<Trip>();
    DbSet<Point> Points => Set<Point>();
    DbSet<TripPoints> TripPoints => Set<TripPoints>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        // um user tem várias viagens 1:N
        // uma viagem tem vários pontos 
        // um ponto tem várias viagens e N:N 

        model.Entity<Trip>()
            .HasOne(t => t.User)
            .WithMany(u => u.Trips)
            .HasForeignKey(u => u.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<TripPoints>()
            .HasOne(u => u.User)
            .WithMany(u => u.TripPoints)
            .HasForeignKey(u => u.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<TripPoints>()
            .HasOne(t => t.Trip)
            .WithMany(t => t.TripPoints)
            .HasForeignKey(t => t.TripID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<TripPoints>()
            .HasOne(t => t.Point)
            .WithMany(t => t.TripPoints)
            .HasForeignKey(t => t.PointID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}