using System;
using System.Linq;
using ivr.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using ivr;
namespace ivr.Services
{
    public class ItemDataStore : IDataStore<Item>
    {
        static SQLiteAsyncConnection Database;

        public ItemDataStore()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<Item>();
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            return await Database.DeleteAsync<Item>(id);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Database.GetAsync<Item>(id);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Database.Table<Item>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                await Database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                await Database.InsertAsync(item);
                return (await GetItemsAsync()).LastOrDefault().Id;
            }
        }
    }
}