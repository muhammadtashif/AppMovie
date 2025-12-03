using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieRental.Shared;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("rating")]
    public double Rating { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("genre")]
    public string Genre { get; set; } = string.Empty;

    [BsonElement("releaseYear")]
    public int ReleaseYear { get; set; }
}
