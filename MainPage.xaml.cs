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
            // 1. Add — create one test product (id = 1 )
             await _productRepository.AddAsync(new Product { Name = "Flour", Category = "Grocery", Price = 60, Quantity = 25, Unit = "kg" });

            // 2. GetAll — fetch everything, check DB Browser after this runs (id = 2)
            var all = await _productRepository.GetAllAsync();

            //// 3. GetById — grab the one you just added (use its actual id from DB Browser, or all.FirstOrDefault()) (id = 3)
            var one = await _productRepository.GetByIdAsync(1);

            //// 4. Update — change something on it, then save (price 85, id = 1)
            if (one != null)
            {
                one.Price = 85;
                await _productRepository.UpdateAsync(one);
            }

            //// 5. Delete — remove it (row deleted with id = 1, price = 85. and one row added with id 5. so id is now 2,3,4,5)
            await _productRepository.DeleteAsync(1);

            // last whole run (rows: 2,3,4,5,6 all with price = 60)
        }
    }
}
