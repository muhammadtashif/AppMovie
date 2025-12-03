using Microsoft.JSInterop;
using MovieRental.Shared;
using System.Text.Json;

namespace MovieRental.Client.Services;

public class IndexedDbService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<IndexedDbService> _logger;
    private const string DB_NAME = "MovieRentalDB";
    private const string STORE_NAME = "rentals";

    public IndexedDbService(IJSRuntime jsRuntime, ILogger<IndexedDbService> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("indexedDbHelper.initDB", DB_NAME, STORE_NAME);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing IndexedDB");
        }
    }

    public async Task<bool> AddRentalAsync(Movie movie)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(movie.Id))
            {
                _logger.LogWarning("Attempted to add rental with empty movie Id");
                return false;
            }

            var rental = new Rental
            {
                MovieId = movie.Id ?? string.Empty,
                MovieTitle = movie.Title,
                RentedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                Price = movie.Price,
                ImageUrl = movie.ImageUrl
            };

            var rentalJson = JsonSerializer.Serialize(rental);
            await _jsRuntime.InvokeVoidAsync("indexedDbHelper.addRental", DB_NAME, STORE_NAME, rental.MovieId, rentalJson);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding rental to IndexedDB");
            return false;
        }
    }

    public async Task<List<Rental>> GetAllRentalsAsync()
    {
        try
        {
            var rentalsJson = await _jsRuntime.InvokeAsync<string[]>("indexedDbHelper.getAllRentals", DB_NAME, STORE_NAME);
            var rentals = new List<Rental>();

            foreach (var json in rentalsJson)
            {
                var rental = JsonSerializer.Deserialize<Rental>(json);
                if (rental != null)
                {
                    rentals.Add(rental);
                }
            }

            return rentals;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting rentals from IndexedDB");
            return new List<Rental>();
        }
    }

    public async Task<Rental?> GetRentalAsync(string movieId)
    {
        try
        {
            var rentalJson = await _jsRuntime.InvokeAsync<string>("indexedDbHelper.getRental", DB_NAME, STORE_NAME, movieId);
            if (string.IsNullOrEmpty(rentalJson))
                return null;

            return JsonSerializer.Deserialize<Rental>(rentalJson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting rental from IndexedDB");
            return null;
        }
    }

    public async Task RemoveRentalAsync(string movieId)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("indexedDbHelper.removeRental", DB_NAME, STORE_NAME, movieId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing rental from IndexedDB");
        }
    }

    public async Task RemoveExpiredRentalsAsync()
    {
        try
        {
            var rentals = await GetAllRentalsAsync();
            var now = DateTime.UtcNow;

            foreach (var rental in rentals.Where(r => r.ExpiresAt < now))
            {
                await RemoveRentalAsync(rental.MovieId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing expired rentals");
        }
    }

    public async Task<bool> IsMovieRentedAsync(string movieId)
    {
        try
        {
            var rental = await GetRentalAsync(movieId);
            return rental != null && !rental.IsExpired;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if movie is rented");
            return false;
        }
    }
}
