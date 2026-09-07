using System.Collections.ObjectModel;
using PosApp.Data;
using PosApp.Models;


namespace PosApp.Views
{
    public partial class ProductsPage : ContentPage
    {
        private readonly IProductRepository _productRepository;
        public ObservableCollection<Product> Products { get; } = new();

        public ProductsPage(IProductRepository productRepository)
        {
            InitializeComponent();
            _productRepository = productRepository;
            BindingContext = this;
        }
        private async Task LoadProductsAsync()
        {
            Products.Clear();
            var all = await _productRepository.GetAllAsync();
            foreach (var product in all)
            {
                Products.Add(product);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProductsAsync();

        }
    }
}