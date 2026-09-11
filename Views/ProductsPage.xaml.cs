using PosApp.ViewModels;


namespace PosApp.Views
{
    public partial class ProductsPage : ContentPage
    {
        private readonly ProductsViewModel _viewModel;

        public ProductsPage(ProductsViewModel viewModel)  
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;   
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadCommand.ExecuteAsync(null);
        }
    }
}