namespace MovieRental.Shared;

public class Rental
{
    public string MovieId { get; set; } = string.Empty;
    public string MovieTitle { get; set; } = string.Empty;
    public DateTime RentedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    public TimeSpan TimeRemaining => ExpiresAt - DateTime.UtcNow;
}
