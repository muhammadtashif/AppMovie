# MongoDB Installation Complete! ✅

## What Was Done:

1. ✅ **Installed MongoDB Community Server** (version 8.2.2)
2. ✅ **Created data directory** at `C:\data\db`
3. ✅ **Started MongoDB service** - Running on port 27017
4. ✅ **Restored MongoDB code** in the application
5. ✅ **Seeded sample data** - 8 movies automatically added

## MongoDB Service Status:

- **Status**: Running ✅
- **Port**: 27017
- **Database Name**: MovieRentalDb
- **Connection String**: mongodb://localhost:27017

## How to View Your Data:

### Method 1: Using MongoDB Compass (GUI - Easiest)

MongoDB Compass is already installed on your PC!

1. **Open MongoDB Compass** from Start Menu
2. **Connection String**: `mongodb://localhost:27017`
3. Click **Connect**
4. You'll see your database: **MovieRentalDb**
5. Click on **MovieRentalDb** → **movies** collection
6. You'll see all 8 movies with their data:
   - Title
   - Rating
   - Price
   - Description
   - Genre
   - Release Year
   - Image URL

### Method 2: Using MongoDB Shell (Command Line)

```powershell
# Open MongoDB shell
mongosh

# Switch to your database
use MovieRentalDb

# View all movies
db.movies.find().pretty()

# Count movies
db.movies.countDocuments()

# Find a specific movie
db.movies.findOne({title: "The Matrix"})

# Find by genre
db.movies.find({genre: "Sci-Fi"}).pretty()

# Exit
exit
```

### Method 3: Using the API (Swagger)

1. Open browser to: `https://localhost:7001/swagger`
2. You'll see all API endpoints
3. Try **GET /api/movies** to see all movies
4. Try **GET /api/movies/{id}** to get a specific movie
5. Try **GET /api/movies/search** with query parameters

## Viewing User Rental Data:

User rentals are stored in **IndexedDB** (browser storage), not MongoDB.

To view user rental data:

1. **Run the client application**
2. **Open Browser DevTools** (F12)
3. Go to **Application** tab (Chrome) or **Storage** tab (Firefox)
4. Navigate to: **IndexedDB** → **MovieRentalDB** → **rentals**
5. You'll see:
   - movieId
   - movieTitle
   - rentedAt (timestamp)
   - expiresAt (timestamp - 24 hours from rental)
   - price

## MongoDB Service Management:

```powershell
# Check status
Get-Service MongoDB

# Start service
Start-Service MongoDB

# Stop service
Stop-Service MongoDB

# Restart service
Restart-Service MongoDB
```

## Useful MongoDB Commands:

```powershell
# View all databases
mongosh --eval "show dbs"

# Backup your database
mongodump --db MovieRentalDb --out C:\backup

# Restore database
mongorestore --db MovieRentalDb C:\backup\MovieRentalDb

# Drop database (careful!)
mongosh --eval "use MovieRentalDb; db.dropDatabase()"
```

## Current Data in MongoDB:

Your **MovieRentalDb** database contains:

**Collection: movies** (8 documents)
1. The Matrix (Sci-Fi, $4.99, ⭐8.7)
2. Inception (Sci-Fi, $5.99, ⭐8.8)
3. The Dark Knight (Action, $5.99, ⭐9.0)
4. Interstellar (Sci-Fi, $4.99, ⭐8.6)
5. Pulp Fiction (Crime, $3.99, ⭐8.9)
6. The Shawshank Redemption (Drama, $3.99, ⭐9.3)
7. Forrest Gump (Drama, $4.99, ⭐8.8)
8. Avatar (Sci-Fi, $5.99, ⭐7.8)

## Next Steps:

1. **Open MongoDB Compass** to explore your data visually
2. **Run the client** to test the full application
3. **Rent some movies** and see them in IndexedDB
4. **Use Swagger** at https://localhost:7001/swagger to test the API

## Troubleshooting:

**If MongoDB stops working:**
```powershell
# Restart the service
Restart-Service MongoDB

# Check if it's running
Get-Service MongoDB

# Check logs
Get-Content "C:\Program Files\MongoDB\Server\8.0\log\mongod.log" -Tail 50
```

**If you need to reset the database:**
```powershell
mongosh
use MovieRentalDb
db.movies.deleteMany({})
# Then restart your server to re-seed
```

Enjoy your fully functional Movie Rental App with MongoDB! 🎬🎉
