using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluToDoApp.Models;

namespace FluToDoApp.Data
{
    public class FakeToDoApiServiceAgent : IToDoApiServiceAgent
    {
        public Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            var list = new List<ToDoItem>
            {
                new ToDoItem { Key = "1", Name = "Item 1", IsComplete = false },
                new ToDoItem { Key = "2", Name = "Item 2", IsComplete = false },
                new ToDoItem { Key = "3", Name = "Item 3", IsComplete = true },
                new ToDoItem { Key = "4", Name = "Item 4", IsComplete = false },
            };
            return Task.FromResult(list);
        }
    }
}
