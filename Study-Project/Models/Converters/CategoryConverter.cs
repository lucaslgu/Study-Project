using Study_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models.Converters
{
    public class CategoryConverter
    {
        public Category CategoryViewEntity(CategoryViewModel categoryViewModel)
        {
            var category = new Category(categoryViewModel.Id, categoryViewModel.Name);

            return category;
        }

        public CategoryViewModel CategoryEntityView(Category category)
        {
            var model = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.ToList()
            };

            return model;
        }
    }
}
