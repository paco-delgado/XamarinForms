using FluToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.ViewModels
{
    public class ToDoItemViewModel : BaseViewModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        // Default constructor for scenarios where we want to instantiate a 
        // blank ToDoItemViewModel object.
        public ToDoItemViewModel() { }

        public ToDoItemViewModel(ToDoItem toDoItem)
        {
            Key = toDoItem.Key;
            Name = toDoItem.Name;
            IsComplete = toDoItem.IsComplete;
        }
    }
}
