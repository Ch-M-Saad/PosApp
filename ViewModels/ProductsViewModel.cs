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

        public bool IsEditMode => SelectedProduct != null;

        [ObservableProperty]
        private Product? _selectedProduct;

        partial void OnSelectedProductChanged(Product? value)
        {
            if (value != null)
            {
                Name = value.Name;
                Category = value.Category;
                PriceText = value.Price.ToString();
                QuantityText = value.Quantity.ToString();
                Unit = value.Unit;
            }
            OnPropertyChanged(nameof(IsEditMode));
        }

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _category = string.Empty;

        [ObservableProperty]
        private string _priceText = string.Empty;   

        [ObservableProperty]
        private string _quantityText = string.Empty;   

        [ObservableProperty]
        private string _unit = string.Empty;

        [RelayCommand]
        private void Clear()
        {
            Name = string.Empty;
            Category = string.Empty;
            PriceText = string.Empty;
            QuantityText = string.Empty;
            Unit = string.Empty;
            SelectedProduct = null;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (SelectedProduct == null)
            {
                var product = new Product
                {
                    Name = Name,
                    Category = Category,
                    Price = decimal.Parse(PriceText),
                    Quantity = int.Parse(QuantityText),
                    Unit = Unit
                };

                await _productRepository.AddAsync(product);
            }
            else
            {
                SelectedProduct.Name = Name;
                SelectedProduct.Category = Category;
                SelectedProduct.Price = decimal.Parse(PriceText);
                SelectedProduct.Quantity = int.Parse(QuantityText);
                SelectedProduct.Unit = Unit;

                await _productRepository.UpdateAsync(SelectedProduct);
            }

                await LoadCommand.ExecuteAsync(null);
                SelectedProduct = null;

                Name = string.Empty;
                Category = string.Empty;
                PriceText = string.Empty;
                QuantityText = string.Empty;
                Unit = string.Empty;

        }
        


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