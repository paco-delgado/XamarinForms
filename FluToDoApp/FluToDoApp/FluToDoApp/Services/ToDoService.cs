using FluToDoApp.Models;
using FluToDoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoApiServiceAgent _toDoApiServiceAgent;

        public ToDoService(IToDoApiServiceAgent toDoApiServiceAgent)
        {
            _toDoApiServiceAgent = toDoApiServiceAgent;
        }

        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            return await _toDoApiServiceAgent.GetAllToDoItemsAsync();
        }
        public async Task AddToDoItemAsync(ToDoItem item)
        {
            await _toDoApiServiceAgent.AddToDoItemAsync(item);
        }

        public async Task DeleteToDoItemAsync(string key)
        {
            await _toDoApiServiceAgent.DeleteToDoItemAsync(key);
        }
    }
}
