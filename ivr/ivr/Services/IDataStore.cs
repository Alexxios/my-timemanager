    using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ivr.Services
{
    public interface IDataStore<T>
    {
        Task<int> SaveItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
