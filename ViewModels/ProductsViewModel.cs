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

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _category = string.Empty;

        [ObservableProperty]
        private string _priceText = string.Empty;   // (1) price, as a string

        [ObservableProperty]
        private string _quantityText = string.Empty;   // (2) quantity, as a string

        [ObservableProperty]
        private string _unit = string.Empty;

        private readonly IProductRepository _productRepository;   

        [ObservableProperty]
        private bool _isBusy;

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();   

        public ProductsViewModel(IProductRepository productRepository)  
        {
            _productRepository = productRepository;
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
             IsBusy= true;                     

            Products.Clear();                 

            var products = await _productRepository.GetAllAsync();   

            foreach (var p in products)
            {
                Products.Add(p);            
            }

            IsBusy = false;                   
        }
    }
}