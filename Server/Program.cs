using MongoDB.Driver;
using MovieRental.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Response Caching
builder.Services.AddResponseCaching();

// Add Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Configure MongoDB with optimized settings
var mongoConnectionString = builder.Configuration["MongoDB:ConnectionString"] ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "MovieRentalDb";

// Configure MongoDB client with timeout and pooling settings
var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);
mongoClientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
mongoClientSettings.ConnectTimeout = TimeSpan.FromSeconds(5);
mongoClientSettings.SocketTimeout = TimeSpan.FromSeconds(10);
mongoClientSettings.MaxConnectionPoolSize = 100;
mongoClientSettings.MinConnectionPoolSize = 10;

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient(mongoClientSettings);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoDatabaseName);
});

// Configure CORS for Blazor WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins(
            "https://localhost:5001",
            "http://localhost:5000",
            "https://localhost:7001",
            "http://localhost:5173",
            "http://localhost:5197",
            "https://localhost:7021",
            "http://localhost:5187",
            "https://localhost:7207"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Cache-Control");
    });
});

var app = builder.Build();

// Seed database and create indexes
using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    try
    {
        await DbSeeder.SeedData(database);
        await MongoDbIndexes.CreateIndexesAsync(database);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogWarning(ex, "Could not seed MongoDB database or create indexes.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Use Response Compression
app.UseResponseCompression();

// Use Response Caching
app.UseResponseCaching();

app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();


