﻿using FluToDoApp.ViewModels;
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
    public partial class ToDoItemDetailPage : ContentPage
    {
        public ToDoItemDetailPage(ToDoItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
