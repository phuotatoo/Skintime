using Skintime.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skintime.Data
{
    public class InventoryDatabase
    {
        static SQLiteAsyncConnection Inventory;

        public InventoryDatabase(string dbPath)
        {
            Inventory = new SQLiteAsyncConnection(dbPath);
            Inventory.CreateTableAsync<Cosmetics>().Wait();
        }

        /* them san pham
        public Task<int> InsertItem(Cosmetics item)
        {
            return database.InsertAsync(item);
        }*/

        public Task<List<Product>> GetItem()
        {
            // hien toan bo san pham
            return Inventory.Table<Product>().ToListAsync();
        }

        public Task<int> SaveNoteAsync(Product myProduct)
        {
            if (myProduct.ID != 0)
            {
                // Update an existing note.
                return Inventory.UpdateAsync(myProduct);
            }
            else
            {
                // Save a new note.
                return Inventory.InsertAsync(myProduct);
            }
        }

        public Task<int> DeleteItem(Product myProduct)
        {
            return Inventory.DeleteAsync(myProduct);
        }
    }
}
