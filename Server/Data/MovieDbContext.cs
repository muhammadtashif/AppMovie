using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using MovieRental.Shared;

namespace MovieRental.Server.Data;

public class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; init; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Movie>().ToCollection("movies");
    }

    public static MovieDbContext Create(IMongoDatabase database)
    {
        var options = new DbContextOptionsBuilder<MovieDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options;

        return new MovieDbContext(options);
    }
}
