using Skintime.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skintime.Data
{
    public class DiaryDatabase
    {
        readonly SQLiteAsyncConnection database;

        public DiaryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Diary>().Wait();
        }

        public Task<List<Diary>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Diary>().ToListAsync();
        }

        public Task<Diary> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Diary>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Diary note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Diary note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }

}
