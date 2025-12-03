window.indexedDbHelper = {
    initDB: function (dbName, storeName) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName, 1);

            request.onerror = () => reject(request.error);
            request.onsuccess = () => resolve(request.result);

            request.onupgradeneeded = (event) => {
                const db = event.target.result;
                if (!db.objectStoreNames.contains(storeName)) {
                        // Use PascalCase key to match C# serialized property names (MovieId)
                        db.createObjectStore(storeName, { keyPath: 'MovieId' });
                    }
            };
        });
    },

    addRental: async function (dbName, storeName, movieId, rentalJson) {
        const db = await this.initDB(dbName, storeName);
        const rental = JSON.parse(rentalJson);

        return new Promise((resolve, reject) => {
            const transaction = db.transaction([storeName], 'readwrite');
            const store = transaction.objectStore(storeName);
            const request = store.put(rental);

            request.onsuccess = () => resolve();
            request.onerror = () => reject(request.error);
        });
    },

    getRental: async function (dbName, storeName, movieId) {
        const db = await this.initDB(dbName, storeName);

        return new Promise((resolve, reject) => {
            const transaction = db.transaction([storeName], 'readonly');
            const store = transaction.objectStore(storeName);
            const request = store.get(movieId);

            request.onsuccess = () => {
                if (request.result) {
                    resolve(JSON.stringify(request.result));
                } else {
                    resolve(null);
                }
            };
            request.onerror = () => reject(request.error);
        });
    },

    getAllRentals: async function (dbName, storeName) {
        const db = await this.initDB(dbName, storeName);

        return new Promise((resolve, reject) => {
            const transaction = db.transaction([storeName], 'readonly');
            const store = transaction.objectStore(storeName);
            const request = store.getAll();

            request.onsuccess = () => {
                const rentals = request.result.map(r => JSON.stringify(r));
                resolve(rentals);
            };
            request.onerror = () => reject(request.error);
        });
    },

    removeRental: async function (dbName, storeName, movieId) {
        const db = await this.initDB(dbName, storeName);

        return new Promise((resolve, reject) => {
            const transaction = db.transaction([storeName], 'readwrite');
            const store = transaction.objectStore(storeName);
            const request = store.delete(movieId);

            request.onsuccess = () => resolve();
            request.onerror = () => reject(request.error);
        });
    }
};

// Auto-cleanup expired rentals on page load
window.addEventListener('load', async () => {
    try {
        const dbName = 'MovieRentalDB';
        const storeName = 'rentals';
        const db = await window.indexedDbHelper.initDB(dbName, storeName);

        const transaction = db.transaction([storeName], 'readwrite');
        const store = transaction.objectStore(storeName);
        const request = store.getAll();

        request.onsuccess = () => {
            const now = new Date();
            request.result.forEach(rental => {
                try {
                    // rental.ExpiresAt is serialized from C# as PascalCase
                    const expires = new Date(rental.ExpiresAt);
                    if (!isNaN(expires) && expires < now) {
                        store.delete(rental.MovieId);
                    }
                } catch (e) {
                    console.error('Error parsing rental expiresAt', e);
                }
            });
        };
    } catch (error) {
        console.error('Error cleaning up expired rentals:', error);
    }
});
