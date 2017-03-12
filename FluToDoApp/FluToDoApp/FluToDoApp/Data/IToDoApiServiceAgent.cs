using System.Collections.Generic;
using System.Threading.Tasks;
using FluToDoApp.Models;

namespace FluToDoApp.Data
{
    public interface IToDoApiServiceAgent
    {
        Task<List<ToDoItem>> GetAllToDoItemsAsync();
    }
}