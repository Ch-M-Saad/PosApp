using PosApp.Data;
using PosApp.Models;

namespace PosApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IProductRepository _productRepository;

        public MainPage(IProductRepository productRepository)
        {
            InitializeComponent();
            _productRepository = productRepository;
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            await _productRepository.AddAsync(new Product { Name = "Rice", Category = "Grocery", Price = 120, Quantity = 50, Unit = "kg" });
            await _productRepository.AddAsync(new Product { Name = "Oil", Category = "Grocery", Price = 300, Quantity = 20, Unit = "litre" });
            await _productRepository.AddAsync(new Product { Name = "Rice'); DROP TABLE Products; --", Category = "Test", Price = 1, Quantity = 1, Unit = "piece" });

            var all = await _productRepository.GetAllAsync();
            await DisplayAlert("Test", $"Products in DB: {all.Count}", "OK");
        }
    }
}
