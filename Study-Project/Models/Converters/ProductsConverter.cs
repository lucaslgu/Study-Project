using Study_Project.Models.ViewModels;
using Study_Project.Services;
using System.Threading.Tasks;

namespace Study_Project.Models.Converters
{
    public class ProductsConverter
    {
        private readonly CategoriesService _categoriesService;

        public ProductsConverter(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public Product ProductViewEntity(ProductViewModel productViewModel, int categoryId)
        {
            var product = new Product(productViewModel.Id, productViewModel.Description, productViewModel.Value, categoryId);
            return product;
        }

        public async Task<ProductViewModel> ProductEntityView(Product product)
        {
            var model = new ProductViewModel
            {
                Id = product.Id,
                Value = product.Value,
                Description = product.Description,
                Category = new Category(product.CategoryId, product.Category.Name),
                CategorySelect = await _categoriesService.GetCategoriesAsync()
            };

            return model;
        }
    }
}
