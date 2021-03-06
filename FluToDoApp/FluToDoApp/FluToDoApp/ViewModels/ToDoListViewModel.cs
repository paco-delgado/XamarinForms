﻿using FluToDoApp.Models;
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

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetValue(ref _isRefreshing, value);
            }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddToDoItemCommand { get; private set; }
        public ICommand DeleteToDoItemCommand { get; private set; }
        public ICommand ToggleToDoItemStateCommand { get; private set; }
        public ICommand RefreshDataCommand { get; private set; }

        public ToDoListViewModel(IToDoService toDoService, IPageService pageService)
        {
            _toDoService = toDoService;
            _pageService = pageService;
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            AddToDoItemCommand = new Command(async () => await AddToDoItemAsync());
            DeleteToDoItemCommand = new Command<ToDoItemViewModel>(async i => await DeleteToDoItemAsync(i));
            ToggleToDoItemStateCommand = new Command<ToDoItemViewModel>(async i => await ToggleToDoItemStateAsync(i));
            RefreshDataCommand = new Command(async () =>
            {
                await LoadDataAsync();
                IsRefreshing = false;
            });
        }

        private async Task LoadDataAsync()
        {
            if (_isDataLoaded && !_isRefreshing)
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

        private async Task DeleteToDoItemAsync(ToDoItemViewModel toDoItemViewModel)
        {
            if (await _pageService.DisplayAlertAsync("Delete", $"Are you sure you want to delete {toDoItemViewModel.Name}?", "Yes", "No"))
            {
                ToDoItems.Remove(toDoItemViewModel);
                await _toDoService.DeleteToDoItemAsync(toDoItemViewModel.Key);
            }
        }

        private async Task ToggleToDoItemStateAsync(ToDoItemViewModel toDoItemViewModel)
        {
            toDoItemViewModel.IsComplete = !toDoItemViewModel.IsComplete;
            var toDoItem = new ToDoItem
            {
                Key = toDoItemViewModel.Key,
                Name = toDoItemViewModel.Name,
                IsComplete = toDoItemViewModel.IsComplete
            };
            await _toDoService.UpdateToDoItemStateAsync(toDoItem);
        }
    }
}
