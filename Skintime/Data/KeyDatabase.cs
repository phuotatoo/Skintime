using Skintime.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skintime.Data
{
    public class KeyDatabase
    {
        readonly SQLiteAsyncConnection database;
        public KeyDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Key>().Wait();
        }

        public Task<List<Key>> GetKeyAsync()
        {
            //Get all notes.
            return database.Table<Key>().ToListAsync();
        }

        public Task<Key> GetKeyAsync(string check)
        {
            // Get a specific note.
            return database.Table<Key>()
                            .Where(i => i.name == check || i.brand == check)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveKeyAsync(Key note)
        {
            return database.InsertAsync(note);
        }
    }
}
