using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Skintime.Models;
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

        public Task<Key> GetKeyAsync(string key)
        {
            // Get a specific note.
            return database.Table<Key>()
                            .Where(i => i.productname == key)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveKeyAsync(Key product)
        {
            return database.InsertAsync(product);
        }
    }
}
