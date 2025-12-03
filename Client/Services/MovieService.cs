using System.Net.Http.Json;
using MovieRental.Shared;
using Microsoft.AspNetCore.Components;

namespace MovieRental.Client.Services;

public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MovieService> _logger;
    private readonly NavigationManager _navigationManager;
    
    // Client-side cache
    private static List<Movie>? _cachedMovies;
    private static DateTime _cacheExpiry = DateTime.MinValue;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public MovieService(HttpClient httpClient, ILogger<MovieService> logger, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _logger = logger;
        _navigationManager = navigationManager;
        
        // Set timeout to allow MongoDB queries to complete
        _httpClient.Timeout = TimeSpan.FromSeconds(15);
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        // Return cached data if still valid
        if (_cachedMovies != null && DateTime.UtcNow < _cacheExpiry)
        {
            _logger.LogInformation("Returning cached movies");
            return _cachedMovies;
        }

        try
        {
            // Fetch from API with increased timeout
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("api/movies");
            if (movies != null && movies.Count > 0)
            {
                // Cache the result
                _cachedMovies = movies;
                _cacheExpiry = DateTime.UtcNow.Add(CacheDuration);
                return movies;
            }

            // Return empty list if API returns no data
            _logger.LogWarning("API returned empty movie list");
            return new List<Movie>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movies from API");
            // Return empty list on error
            return new List<Movie>();
        }
    }


    public async Task<Movie?> GetMovieByIdAsync(string id)
    {
        // Check cache first
        if (_cachedMovies != null && DateTime.UtcNow < _cacheExpiry)
        {
            var cached = _cachedMovies.FirstOrDefault(m => m.Id == id);
            if (cached != null)
                return cached;
        }

        try
        {
            var movie = await _httpClient.GetFromJsonAsync<Movie>($"api/movies/{id}");
            return movie;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movie {MovieId} from API", id);
            return null;
        }
    }

    public async Task<List<Movie>> SearchMoviesAsync(string? genre = null, string? title = null)
    {
        try
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrWhiteSpace(genre))
                queryParams.Add($"genre={Uri.EscapeDataString(genre)}");
            if (!string.IsNullOrWhiteSpace(title))
                queryParams.Add($"title={Uri.EscapeDataString(title)}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>($"api/movies/search{queryString}");
            return movies ?? new List<Movie>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching movies");
            return new List<Movie>();
        }
    }
    
    
    // Method to clear cache (useful for refresh)
    public void ClearCache()
    {
        _cachedMovies = null;
        _cacheExpiry = DateTime.MinValue;
    }
}
