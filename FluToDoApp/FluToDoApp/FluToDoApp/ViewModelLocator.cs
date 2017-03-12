using FluToDoApp.Data.Helper;
using FluToDoApp.Data;
using FluToDoApp.Services;
using FluToDoApp.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace FluToDoApp
{
    public class ViewModelLocator
    {
        private readonly UnityContainer _container;

        public ToDoListViewModel ToDoListViewModel
        {
            get
            {
                return _container.Resolve<ToDoListViewModel>();
            }

        }

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<IHttpClientWrapper, HttpClientWrapper>();
            _container.RegisterType<IToDoApiServiceAgent, ToDoApiServiceAgent>();
            _container.RegisterType<IToDoService, ToDoService>();
            _container.RegisterType<IPageService, PageService>();
            _container.RegisterType<ToDoListViewModel>(new ContainerControlledLifetimeManager());
            var unityServiceLocator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }
    }
}
