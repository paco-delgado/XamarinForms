using FluToDoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FluToDoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoListPage : ContentPage
    {
        public ToDoListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await Task.Run(() => ((ToDoListViewModel)BindingContext).LoadDataCommand.Execute(null));
        }
    }
}
