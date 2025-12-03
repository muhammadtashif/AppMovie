# ✅ Fix Applied: Duplicate Image

**Issue:** "Forrest Gump" and "Avatar" were using the same image.
**Cause:** The image URL for Forrest Gump was set to the same URL as Avatar.

---

## 🛠️ Changes Made

1. **Updated Database Seeder (`DbSeeder.cs`)**
   - Changed the Forrest Gump image to a unique, drama-themed image (a park bench).
   - Updated both the **automatic update logic** (for existing databases) and the **seed list** (for new databases).

---

## 🚀 Action Required

Please **restart the Server** for the database update to apply.

1. Stop the running Server (Ctrl+C).
2. Start the Server again:
   ```powershell
   cd "c:\Users\Administrator\Movie App\MovieRental\Server"
   dotnet run
   ```
3. Refresh the browser. Forrest Gump should now have a unique image!
