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

        private bool _isComplete;
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                SetValue(ref _isComplete, value);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
            }
        }

        // Default constructor for scenarios where we want to instantiate a 
        // blank ToDoItemViewModel object.
        public ToDoItemViewModel() { }

        public ToDoItemViewModel(ToDoItem toDoItem)
        {
            Key = toDoItem.Key;
            // We use the private field directly when initializing the object
			// because we don't want this to raise a notification. 
            _name = toDoItem.Name;
            _isComplete = toDoItem.IsComplete;
        }
    }
}
