using Skintime.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skintime.Data
{
    public class InventoryDatabase
    {
        readonly SQLiteAsyncConnection database;
        public InventoryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<KetQua>().Wait();
        }

        public Task<List<KetQua>> GetKeyAsync()
        {
            //Get all notes.
            return database.Table<KetQua>().ToListAsync();
        }

        public Task<KetQua> GetKeyAsync(int id)
        {
            // Get a specific note.
            return database.Table<KetQua>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveKeyAsync(KetQua res)
        {
            return database.InsertAsync(res);
        }

        public Task<int> DeleteKeyAsync(KetQua res)
        {
            // Delete a note.
            return database.DeleteAsync(res);
        }

        public Task<int> DeleteAll()
        {
            return database.DeleteAllAsync<KetQua>();
        }
    }
}
