# Quick Start Guide

## Prerequisites Check

1. ✅ .NET 9.0 SDK installed
2. ✅ MongoDB installed and running
3. ✅ Modern web browser

## Installation Steps

### Step 1: Start MongoDB

```powershell
# Start MongoDB service
net start MongoDB

# OR run mongod directly
mongod --dbpath C:\data\db
```

### Step 2: Restore Packages

```powershell
cd "C:\Users\Administrator\Movie App"
dotnet restore MovieRental.sln
```

### Step 3: Run the Server

```powershell
cd MovieRental/Server
dotnet run
```

Keep this terminal open. The API will be available at `https://localhost:7001`

### Step 4: Run the Client (New Terminal)

```powershell
cd "C:\Users\Administrator\Movie App\MovieRental\Client"
dotnet run
```

The Blazor app will open in your browser automatically.

## What to Expect

1. **Home Page**: You'll see 8 sample movies in a beautiful grid
2. **Click "Watch Now"**: The movie will be added to your watch list for 24 hours
3. **Navigate to "Watch List"**: See your rented movies with countdown timers
4. **Wait 24 hours (or modify IndexedDB)**: Movies will automatically expire and be removed

## Testing Expiration

To test the 24-hour expiration without waiting:

1. Rent a movie
2. Open browser DevTools (F12)
3. Go to Application > IndexedDB > MovieRentalDB > rentals
4. Find your rental and modify the `expiresAt` date to a past date
5. Refresh the page - the rental should be removed automatically

## Troubleshooting

**MongoDB not running?**
```powershell
# Check if MongoDB is running
Get-Service MongoDB

# Start it
net start MongoDB
```

**Port already in use?**
- Check if another application is using ports 7001 or 5001
- Modify the ports in launchSettings.json if needed

**NuGet restore fails?**
- Check your internet connection
- Try: `dotnet nuget locals all --clear`
- Then: `dotnet restore` again

## Next Steps

- Explore the code in Visual Studio or VS Code
- Modify the CSS to customize the design
- Add more movies to the database
- Implement user authentication
- Add payment processing
- Deploy to Azure or AWS

Enjoy your Movie Rental App! 🎬
