# Movie Rental App

A modern movie rental application built with **Blazor WebAssembly**, **ASP.NET Core Web API**, **MongoDB**, and **IndexedDB**.

## 🎯 Features

- **Browse Movies**: View a beautiful grid of available movies with ratings, prices, and descriptions
- **24-Hour Rental**: Rent movies that become available in your watch list for 24 hours
- **Local Storage**: Rentals are stored in browser's IndexedDB for offline access
- **Auto-Expiration**: Movies automatically expire and are removed after 24 hours
- **Modern UI**: Beautiful dark theme with gradients, animations, and responsive design

## 🏗️ Architecture

### Projects

1. **MovieRental.Shared**: Shared models (Movie, Rental)
2. **MovieRental.Server**: ASP.NET Core Web API with MongoDB
3. **MovieRental.Client**: Blazor WebAssembly frontend

### Technologies

- **Frontend**: Blazor WebAssembly
- **Backend**: ASP.NET Core 9.0 Web API
- **Database**: MongoDB with Entity Framework Core
- **Local Storage**: IndexedDB (via JavaScript Interop)
- **Styling**: Custom CSS with modern design system

## 📋 Prerequisites

- .NET 9.0 SDK
- MongoDB (running on localhost:27017)
- Modern web browser with IndexedDB support

## 🚀 Getting Started

### 1. Install MongoDB

Download and install MongoDB from [mongodb.com](https://www.mongodb.com/try/download/community)

Start MongoDB service:
```bash
# Windows
net start MongoDB

# Or run mongod directly
mongod --dbpath C:\data\db
```

### 2. Restore NuGet Packages

```bash
cd "C:\Users\Administrator\Movie App"
dotnet restore MovieRental.sln
```

### 3. Run the Application

#### Option A: Run Both Projects Separately

**Terminal 1 - Start the API Server:**
```bash
cd MovieRental/Server
dotnet run
```
The API will start at `https://localhost:7001`

**Terminal 2 - Start the Blazor Client:**
```bash
cd MovieRental/Client
dotnet run
```
The client will start at `https://localhost:5001` (or similar)

#### Option B: Use Visual Studio

1. Open `MovieRental.sln`
2. Set multiple startup projects (Server and Client)
3. Press F5 to run

### 4. Access the Application

Open your browser and navigate to the Blazor client URL (typically `https://localhost:5001`)

## 📁 Project Structure

```
MovieRental/
├── Shared/
│   ├── Movie.cs              # Movie model with MongoDB attributes
│   └── Rental.cs             # Rental model for IndexedDB
├── Server/
│   ├── Controllers/
│   │   └── MoviesController.cs   # API endpoints
│   ├── Data/
│   │   ├── MovieDbContext.cs     # EF Core MongoDB context
│   │   └── DbSeeder.cs           # Sample data seeder
│   ├── Program.cs                # API configuration
│   └── appsettings.json          # MongoDB connection string
└── Client/
    ├── Pages/
    │   ├── Home.razor            # Movie list page
    │   └── WatchList.razor       # Rented movies page
    ├── Services/
    │   ├── MovieService.cs       # API client
    │   └── IndexedDbService.cs   # IndexedDB wrapper
    ├── Layout/
    │   ├── MainLayout.razor      # Main layout
    │   └── NavMenu.razor         # Navigation
    ├── wwwroot/
    │   ├── indexeddb.js          # IndexedDB JavaScript interop
    │   ├── css/app.css           # Modern styling
    │   └── appsettings.json      # API base URL
    └── Program.cs                # Client configuration
```

## 🎨 Features in Detail

### Home Page
- Displays all available movies in a responsive grid
- Shows movie title, rating, price, genre, and description
- "Watch Now" button to rent a movie
- Visual feedback when a movie is already rented

### Watch List Page
- Shows all rented movies
- Displays rental information (rented time, expiration time, time remaining)
- Auto-refreshes every minute to update time remaining
- Automatically removes expired rentals
- Visual distinction between active and expired rentals

### IndexedDB Integration
- Movies are stored locally when rented
- Automatic cleanup of expired rentals on page load
- Persistent across browser sessions
- No server-side user management needed

### MongoDB Integration
- Entity Framework Core for MongoDB
- Automatic database seeding with sample movies
- Clean separation of concerns with DbContext

## 🔧 Configuration

### Server Configuration (appsettings.json)
```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "MovieRentalDb"
  }
}
```

### Client Configuration (wwwroot/appsettings.json)
```json
{
  "ApiBaseAddress": "https://localhost:7001"
}
```

## 🎭 Sample Data

The application automatically seeds 8 movies on first run:
- The Matrix
- Inception
- The Dark Knight
- Interstellar
- Pulp Fiction
- The Shawshank Redemption
- Forrest Gump
- Avatar

## 🐛 Troubleshooting

### MongoDB Connection Issues
- Ensure MongoDB is running: `mongod --version`
- Check connection string in `Server/appsettings.json`
- Verify MongoDB is listening on port 27017

### CORS Issues
- Ensure the Client URL is added to CORS policy in `Server/Program.cs`
- Check browser console for CORS errors

### IndexedDB Issues
- Ensure JavaScript is enabled in your browser
- Check browser console for IndexedDB errors
- Clear browser data if IndexedDB is corrupted

### Build Issues
- Ensure .NET 9.0 SDK is installed: `dotnet --version`
- Restore NuGet packages: `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

## 📝 API Endpoints

- `GET /api/movies` - Get all movies
- `GET /api/movies/{id}` - Get movie by ID
- `GET /api/movies/search?genre={genre}&title={title}` - Search movies

## 🎨 Design System

The application uses a modern design system with:
- **Dark Theme**: Sleek dark background with gradient overlays
- **Color Palette**: Purple/indigo primary colors with pink accents
- **Typography**: Inter font family for clean, modern look
- **Animations**: Smooth transitions and hover effects
- **Responsive**: Works on desktop, tablet, and mobile

## 📄 License

This is a demonstration project for educational purposes.

## 🤝 Contributing

This is a sample project. Feel free to fork and modify as needed!
