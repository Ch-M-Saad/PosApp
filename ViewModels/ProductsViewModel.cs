using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosApp.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;
    }
}