# MongoDB Database Reset Guide

**Purpose:** Clear existing movies and reseed with the new 12-movie collection

---

## 🗑️ Option 1: Using MongoDB Compass (GUI)

1. **Open MongoDB Compass**
2. **Connect** to `mongodb://localhost:27017`
3. **Select** the `MovieRentalDb` database
4. **Select** the `movies` collection
5. **Click** "Delete" button (trash icon)
6. **Confirm** deletion of all documents
7. **Restart** the Server application - it will auto-seed the 12 movies

---

## 🗑️ Option 2: Using MongoDB Shell (Command Line)

### Step 1: Open MongoDB Shell
```powershell
mongosh
```

### Step 2: Switch to Database
```javascript
use MovieRentalDb
```

### Step 3: Delete All Movies
```javascript
db.movies.deleteMany({})
```

### Step 4: Verify Deletion
```javascript
db.movies.countDocuments()
// Should return: 0
```

### Step 5: Exit Shell
```javascript
exit
```

### Step 6: Restart Server
The server will automatically seed 12 movies on startup.

---

## 🗑️ Option 3: Drop Entire Database

If you want to completely reset everything:

```javascript
use MovieRentalDb
db.dropDatabase()
```

Then restart the server to recreate and seed.

---

## ✅ Verify New Data

### After Restarting Server:

**Check in MongoDB Compass:**
- Database: `MovieRentalDb`
- Collection: `movies`
- Document count: **12**

**Check in MongoDB Shell:**
```javascript
use MovieRentalDb
db.movies.countDocuments()
// Should return: 12

db.movies.find({}, {title: 1, rating: 1, price: 1})
// Should show all 12 movies
```

**Check in Application:**
1. Start the client
2. Open browser
3. Should see 12 movie cards including:
   - Gladiator
   - Titanic
   - The Godfather

---

## 🎬 Expected Movies After Reset

1. The Matrix
2. Inception
3. The Dark Knight
4. Interstellar
5. Pulp Fiction
6. The Shawshank Redemption
7. Forrest Gump
8. Avatar
9. The Stranger Things
10. **Gladiator** ⭐ NEW
11. **Titanic** ⭐ NEW
12. **The Godfather** ⭐ NEW

---

## 🚀 Quick Reset Commands

**Full reset in one go:**
```powershell
# Stop the server if running (Ctrl+C)

# Clear database
mongosh --eval "use MovieRentalDb; db.movies.deleteMany({})"

# Start server (will auto-seed)
cd "c:\Users\Administrator\Movie App\MovieRental\Server"
dotnet run
```

---

## ⚠️ Important Notes

1. **Auto-Seeding:** The server automatically seeds data if the collection is empty
2. **No Duplicates:** The seeder checks if data exists before inserting
3. **MongoDB Required:** Make sure MongoDB is running before starting the server
4. **Port:** MongoDB should be on `localhost:27017`

---

**Created:** December 2, 2025  
**Purpose:** Reset database to include 3 new movies
