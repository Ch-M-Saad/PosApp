using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using PosApp.Data;
using PosApp.Models;

namespace PosApp.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductRepository _productRepository;   // (1) what type goes here?

        [ObservableProperty]
        private bool _isBusy;

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();   // (2) how do you initialize an empty one?

        public ProductsViewModel(IProductRepository productRepository)   // (3) constructor parameter type
        {
            _productRepository = productRepository;
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
             IsBusy= true;                     // (4) which property flips on?

            Products.Clear();                 // (5) clear before repopulating — which method?

            var products = await _productRepository.GetAllAsync();   // (6) which repository method loads everything?

            foreach (var p in products)
            {
                Products.Add(p);            // (7) how do you add to an ObservableCollection?
            }

            IsBusy = false;                    // (8) flip busy back off
        }
    }
}