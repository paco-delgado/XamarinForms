using System.Collections.Generic;
using System.Threading.Tasks;
using FluToDoApp.Models;

namespace FluToDoApp.Data
{
    public interface IToDoApiServiceAgent
    {
        Task DeleteToDoItemAsync(string key);
        Task<List<ToDoItem>> GetAllToDoItemsAsync();
        Task SaveToDoItemAsync(ToDoItem item, bool isNewItem = false);
    }
}