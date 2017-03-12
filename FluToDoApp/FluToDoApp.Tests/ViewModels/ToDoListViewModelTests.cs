using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluToDoApp.ViewModels;
using Moq;
using FluToDoApp.Services;
using FluToDoApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using FluToDoApp.Views;

namespace FluToDoApp.Tests.ViewModels
{
    [TestClass]
    public class ToDoListViewModelTests
    {
        private ToDoListViewModel _viewModel;
        private Mock<IToDoService> _toDoServiceMock;
        private Mock<IPageService> _pageServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _toDoServiceMock = new Mock<IToDoService>();
            _pageServiceMock = new Mock<IPageService>();
            _viewModel = new ToDoListViewModel(_toDoServiceMock.Object, _pageServiceMock.Object);
        }

        [TestMethod]
        public void LoadDataCommand_GetsAllItems_SetToDoItemsProperty()
        {
            // Arrange
            var toDoList = new List<ToDoItem>
            {
                new ToDoItem { Key = Guid.NewGuid().ToString(), Name = "Item1", IsComplete = false },
                new ToDoItem { Key = Guid.NewGuid().ToString(), Name = "Item2", IsComplete = true },
                new ToDoItem { Key = Guid.NewGuid().ToString(), Name = "Item3", IsComplete = false },
            };

            _toDoServiceMock.Setup(t => t.GetAllToDoItemsAsync()).ReturnsAsync(toDoList);

            // Act
            _viewModel.LoadDataCommand.Execute(null);

            // Assert
            Assert.AreEqual(toDoList.Count, _viewModel.ToDoItems.Count);
            foreach (var item in _viewModel.ToDoItems)
            {
                Assert.IsTrue(toDoList.Any(i => i.Key == item.Key && i.Name == i.Name && i.IsComplete == item.IsComplete));
            }
        }

        [TestMethod]
        public void AddToDoItemCommand_CallsPageServicePushAsync()
        {
            // Arrange
            Page pushedPage = null;
            _pageServiceMock.Setup(p => p.PushAsync(It.IsAny<Page>())).Callback<Page>(p => pushedPage = p);

            // Act
            _viewModel.AddToDoItemCommand.Execute(null);

            // Assert
            var toDoItemDetailPage = pushedPage as ToDoItemDetailPage;
            Assert.IsNotNull(toDoItemDetailPage);
            Assert.IsNotNull(toDoItemDetailPage.BindingContext);
            _pageServiceMock.Verify(p => p.PushAsync(It.IsAny<Page>()), Times.Once);
        }

        // TODO: Complete unitests for this and the rest of ViewModels
    }
}
