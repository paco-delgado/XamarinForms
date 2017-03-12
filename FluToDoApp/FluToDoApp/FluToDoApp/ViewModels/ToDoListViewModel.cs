using FluToDoApp.Models;
using FluToDoApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using FluToDoApp.Views;

namespace FluToDoApp.ViewModels
{
    public class ToDoListViewModel : BaseViewModel
    {
        private readonly IToDoService _toDoService;
        private IPageService _pageService;
        private bool _isDataLoaded;

        public ObservableCollection<ToDoItemViewModel> ToDoItems { get; private set; } = new ObservableCollection<ToDoItemViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddToDoItemCommand { get; private set; }

        public ToDoListViewModel(IToDoService toDoService, IPageService pageService)
        {
            _toDoService = toDoService;
            _pageService = pageService;
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            AddToDoItemCommand = new Command(async () => await AddToDoItemAsync());
        }

        private async Task LoadDataAsync()
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

        private async Task AddToDoItemAsync()
        {
            var viewModel = new ToDoItemDetailViewModel(new ToDoItemViewModel(), _toDoService, _pageService);

            viewModel.ToDoItemAdded += (source, item) =>
            {
                ToDoItems.Add(new ToDoItemViewModel(item));
            };

            await _pageService.PushAsync(new ToDoItemDetailPage(viewModel));
        }
    }
}
