using PosApp.Data;
using PosApp.Models;

namespace PosApp
{
    public partial class MainPage : ContentPage
    {

        private readonly IProductRepository _productRepository;

        public MainPage(IProductRepository productRepository)
        {
            InitializeComponent();
            _productRepository = productRepository;
        }

        // TODO: remove before Sprint 5 — temporary test scaffolding (Below one)
        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            await _productRepository.AddAsync(new Product { Name = "Sugar", Category = "Grocery", Price = 80, Quantity = 40, Unit = "kg" });

            var all  = await _productRepository.GetAllAsync();
            var firstProduct = all.FirstOrDefault();  // grab whichever product actually exists

            if (firstProduct != null)
            {
                firstProduct.Price = 999;
                await _productRepository.UpdateAsync(firstProduct);
            }
        }
    }
}
