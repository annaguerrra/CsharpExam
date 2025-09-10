using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CSExam.Entites;

public class CSExamDbContext(DbContextOptions opt) : DbContext(opt)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Point> Points => Set<Point>();
    public DbSet<TripPoints> TripPoints => Set<TripPoints>();

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

public class CSExamDbContextFactory : IDesignTimeDbContextFactory<CSExamDbContext>
{
    public CSExamDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CSExamDbContext>();

        // Lê a connection string da variável de ambiente
        var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "A variável de ambiente 'DB_CONNECTION_STRING' não foi definida."
            );
        }

        optionsBuilder.UseSqlServer(connectionString);

        return new CSExamDbContext(optionsBuilder.Options);
    }
}
