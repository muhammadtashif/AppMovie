# ✅ Fix Applied: Connection Error

**Issue:** "No Movies Available" shown in browser.
**Cause:** The Client was trying to connect to the Server on `https://localhost:7137`, but the Server was actually running on `http://localhost:5262`.

---

## 🛠️ Changes Made

1. **Updated Client Configuration**
   - File: `Client/wwwroot/appsettings.json`
   - Change: Set `ApiBaseAddress` to `http://localhost:5262`

2. **Disabled HTTPS Redirection**
   - File: `Server/Program.cs`
   - Change: Commented out `app.UseHttpsRedirection()` to ensure stable HTTP connection.

---

## 🚀 Next Steps

Please **restart both the Server and Client** applications for the changes to take effect.

1. Stop the running applications (Ctrl+C in the terminals).
2. Start the Server:
   ```powershell
   cd "c:\Users\Administrator\Movie App\MovieRental\Server"
   dotnet run
   ```
3. Start the Client:
   ```powershell
   cd "c:\Users\Administrator\Movie App\MovieRental\Client"
   dotnet run
   ```
4. Refresh the browser. You should now see the movies!

---

# ✅ Fix Applied: Missing Image for Forrest Gump

**Issue:** The movie card for "Forrest Gump" was displaying a blank image.
**Cause:** The image URL in the database was invalid or broken.

## 🛠️ Changes Made

1. **Updated Database Seeder**
   - File: `Server/Data/DbSeeder.cs`
   - Change: Updated the image URL for "Forrest Gump" to a valid Unsplash image.
   - Added logic to update the existing record in the database on startup.

## 🚀 Status

The server has been restarted to apply the database update. Please **refresh your browser** to see the corrected image.
