using MovieRental.Shared;

namespace MovieRental.Server.Data;

public static class InMemoryMovieStore
{
    private static List<Movie>? _movies;

    public static List<Movie> GetMovies()
    {
        if (_movies == null)
        {
            SeedMovies();
        }
        return _movies!;
    }

    private static void SeedMovies()
    {
        _movies = new List<Movie>
        {
            new Movie
            {
                Id = "1",
                Title = "The Matrix",
                Rating = 8.7,
                Price = 4.99m,
                Description = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
                Genre = "Sci-Fi",
                ReleaseYear = 1999,
                ImageUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400"
            },
            new Movie
            {
                Id = "2",
                Title = "Inception",
                Rating = 8.8,
                Price = 5.99m,
                Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea.",
                Genre = "Sci-Fi",
                ReleaseYear = 2010,
                ImageUrl = "https://images.unsplash.com/photo-1478720568477-152d9b164e26?w=400"
            },
            new Movie
            {
                Id = "3",
                Title = "The Dark Knight",
                Rating = 9.0,
                Price = 5.99m,
                Description = "When the menace known as the Joker wreaks havoc on Gotham, Batman must accept one of the greatest tests.",
                Genre = "Action",
                ReleaseYear = 2008,
                ImageUrl = "https://images.unsplash.com/photo-1509347528160-9a9e33742cdb?w=400"
            },
            new Movie
            {
                Id = "4",
                Title = "Interstellar",
                Rating = 8.6,
                Price = 4.99m,
                Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                Genre = "Sci-Fi",
                ReleaseYear = 2014,
                ImageUrl = "https://images.unsplash.com/photo-1419242902214-272b3f66ee7a?w=400"
            },
            new Movie
            {
                Id = "5",
                Title = "Pulp Fiction",
                Rating = 8.9,
                Price = 3.99m,
                Description = "The lives of two mob hitmen, a boxer, and a pair of diner bandits intertwine in four tales of violence.",
                Genre = "Crime",
                ReleaseYear = 1994,
                ImageUrl = "https://images.unsplash.com/photo-1485846234645-a62644f84728?w=400"
            },
            new Movie
            {
                Id = "6",
                Title = "The Shawshank Redemption",
                Rating = 9.3,
                Price = 3.99m,
                Description = "Two imprisoned men bond over years, finding solace and eventual redemption through acts of common decency.",
                Genre = "Drama",
                ReleaseYear = 1994,
                ImageUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400"
            },
            new Movie
            {
                Id = "7",
                Title = "Forrest Gump",
                Rating = 8.8,
                Price = 4.99m,
                Description = "The presidencies of Kennedy and Johnson unfold through the perspective of an Alabama man with an IQ of 75.",
                Genre = "Drama",
                ReleaseYear = 1994,
                ImageUrl = "https://images.unsplash.com/photo-1574267432644-f610f5b45f8c?w=400"
            },
            new Movie
            {
                Id = "8",
                Title = "Avatar",
                Rating = 7.8,
                Price = 5.99m,
                Description = "A paraplegic Marine dispatched to the moon Pandora becomes torn between following orders and protecting the world.",
                Genre = "Sci-Fi",
                ReleaseYear = 2009,
                ImageUrl = "https://images.unsplash.com/photo-1451187580459-43490279c0fa?w=400"
            }
        };
    }
}
