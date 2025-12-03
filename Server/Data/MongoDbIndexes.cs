using MongoDB.Driver;
using MovieRental.Shared;

namespace MovieRental.Server.Data;

public static class MongoDbIndexes
{
    public static async Task CreateIndexesAsync(IMongoDatabase database)
    {
        var moviesCollection = database.GetCollection<Movie>("movies");

        // Create index on Title for faster text searches
        var titleIndexModel = new CreateIndexModel<Movie>(
            Builders<Movie>.IndexKeys.Ascending(m => m.Title),
            new CreateIndexOptions { Name = "title_1" }
        );

        // Create index on Genre for faster filtering
        var genreIndexModel = new CreateIndexModel<Movie>(
            Builders<Movie>.IndexKeys.Ascending(m => m.Genre),
            new CreateIndexOptions { Name = "genre_1" }
        );

        // Create index on Rating for sorting
        var ratingIndexModel = new CreateIndexModel<Movie>(
            Builders<Movie>.IndexKeys.Descending(m => m.Rating),
            new CreateIndexOptions { Name = "rating_-1" }
        );

        // Create compound index for genre + rating queries
        var compoundIndexModel = new CreateIndexModel<Movie>(
            Builders<Movie>.IndexKeys
                .Ascending(m => m.Genre)
                .Descending(m => m.Rating),
            new CreateIndexOptions { Name = "genre_1_rating_-1" }
        );

        try
        {
            await moviesCollection.Indexes.CreateManyAsync(new[]
            {
                titleIndexModel,
                genreIndexModel,
                ratingIndexModel,
                compoundIndexModel
            });
        }
        catch (Exception)
        {
            // Indexes might already exist, ignore error
        }
    }
}
