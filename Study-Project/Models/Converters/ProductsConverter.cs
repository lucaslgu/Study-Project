using Study_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models.Converters
{
    public class ProductsConverter
    {
        public Product ProductViewEntity(ProductViewModel productViewModel)
        {
            var product = new Product(productViewModel.Id, productViewModel.Description, productViewModel.Value, productViewModel.CategoryId);
            return product;
        }

        public ProductViewModel ProductEntityView(Product product)
        {
            var model = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                CategoryId = product.CategoryId
            };

            return model;
        }
    }
}
