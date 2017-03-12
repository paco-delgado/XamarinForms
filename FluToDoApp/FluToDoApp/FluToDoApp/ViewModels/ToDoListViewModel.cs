using FluToDoApp.Models;
using FluToDoApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDoApp.ViewModels
{
    public class ToDoListViewModel : BaseViewModel
    {
        private readonly IToDoService _toDoService;
        private bool _isDataLoaded;

        public ObservableCollection<ToDoItemViewModel> ToDoItems { get; private set; } = new ObservableCollection<ToDoItemViewModel>();

        public ICommand LoadDataCommand { get; private set; }

        public ToDoListViewModel(IToDoService toDoService)
        {
            _toDoService = toDoService;
            LoadDataCommand = new Command(async () => await LoadData());
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            List<ToDoItem> toDoItems = await _toDoService.GetAllToDoItemsAsync();

            ToDoItems.Clear();
            foreach (var item in toDoItems)
            {
                ToDoItems.Add(new ToDoItemViewModel(item));
            }
   
        }

    }
}
