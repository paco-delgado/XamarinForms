using FluToDoApp.Models;
using FluToDoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDoApp.ViewModels
{
    public class ToDoItemDetailViewModel : BaseViewModel
    {
        private readonly IToDoService _toDoService;
        private IPageService _pageService;

        public event EventHandler<ToDoItem> ToDoItemAdded;

        public ToDoItem ToDoItem { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ToDoItemDetailViewModel(ToDoItemViewModel viewModel, IToDoService toDoService, IPageService pageService)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            _toDoService = toDoService;
            _pageService = pageService;

            SaveCommand = new Command(async () => await SaveAsync());

            ToDoItem = new ToDoItem
            {
                Key = Guid.NewGuid().ToString(),
                Name = viewModel.Name,
                IsComplete = false
            };
        }

        async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(ToDoItem.Name))
            {
                await _pageService.DisplayAlertAsync("Error", "Please enter the name.", "OK");
                return;
            }

            await _toDoService.AddToDoItemAsync(ToDoItem);

            ToDoItemAdded?.Invoke(this, ToDoItem);

            await _pageService.PopAsync();
        }
    }
}
