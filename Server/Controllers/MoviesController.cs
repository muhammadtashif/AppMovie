using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MovieRental.Shared;
using MovieRental.Server.Data;

namespace MovieRental.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMongoCollection<Movie>? _moviesCollection;
    private readonly ILogger<MoviesController> _logger;
    private readonly bool _useMongoDb;

    public MoviesController(IMongoDatabase? database, ILogger<MoviesController> logger)
    {
        _logger = logger;
        try
        {
            if (database != null)
            {
                _moviesCollection = database.GetCollection<Movie>("movies");
                _useMongoDb = true;
            }
            else
            {
                _useMongoDb = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "MongoDB not available, using in-memory store");
            _useMongoDb = false;
        }
    }

    [HttpGet]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "Accept")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        try
        {
            if (_useMongoDb && _moviesCollection != null)
            {
                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                
                var movies = await _moviesCollection
                    .Find(_ => true)
                    .Limit(100)
                    .ToListAsync(cancellationTokenSource.Token);
                
                return Ok(movies);
            }
            
            // Return error if MongoDB is not available
            _logger.LogError("MongoDB is not configured or available");
            return StatusCode(503, "Database service is unavailable");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movies from MongoDB");
            return StatusCode(500, "An error occurred while fetching movies from the database");
        }
    }

    [HttpGet("{id}")]
    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "id" })]
    public async Task<ActionResult<Movie>> GetMovie(string id)
    {
        try
        {
            if (_useMongoDb && _moviesCollection != null)
            {
                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                
                var movie = await _moviesCollection
                    .Find(m => m.Id == id)
                    .FirstOrDefaultAsync(cancellationTokenSource.Token);
                
                if (movie != null)
                    return Ok(movie);
                
                return NotFound($"Movie with ID {id} not found");
            }
            
            _logger.LogError("MongoDB is not configured or available");
            return StatusCode(503, "Database service is unavailable");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movie with id {MovieId}", id);
            return StatusCode(500, "An error occurred while fetching the movie from the database");
        }
    }

    [HttpGet("search")]
    [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "genre", "title" })]
    public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string? genre, [FromQuery] string? title)
    {
        try
        {
            if (_useMongoDb && _moviesCollection != null)
            {
                var filterBuilder = Builders<Movie>.Filter;
                var filter = filterBuilder.Empty;

                if (!string.IsNullOrWhiteSpace(genre))
                {
                    filter &= filterBuilder.Eq(m => m.Genre, genre);
                }

                if (!string.IsNullOrWhiteSpace(title))
                {
                    filter &= filterBuilder.Regex(m => m.Title, new MongoDB.Bson.BsonRegularExpression(title, "i"));
                }

                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                
                var movies = await _moviesCollection
                    .Find(filter)
                    .Limit(50)
                    .ToListAsync(cancellationTokenSource.Token);
                
                return Ok(movies);
            }
            
            _logger.LogError("MongoDB is not configured or available");
            return StatusCode(503, "Database service is unavailable");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching movies");
            return StatusCode(500, "An error occurred while searching movies in the database");
        }
    }
}


