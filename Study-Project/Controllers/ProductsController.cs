using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Study_Project.Models.Converters;
using Study_Project.Models.ViewModels;
using Study_Project.Services;

namespace Study_Project.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductsServices _productsServices;
        private readonly CategoriesService _categoriesServices;

        private readonly ProductsConverter _converter;

        public ProductsController(ProductsServices productsServices, CategoriesService categoriesServices)
        {
            _productsServices = productsServices;
            _categoriesServices = categoriesServices;
            _converter = new ProductsConverter(categoriesServices);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productsServices.GetProductsAsync();
                var models = new List<ProductViewModel>();
                foreach (var product in products)
                {
                    models.Add(await _converter.ProductEntityView(product));
                }
                return View(models);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _productsServices.GetProductAsync(id.Value);
                if (product == null)
                {
                    return NotFound();
                }

                return View(await _converter.ProductEntityView(product));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesServices.GetCategoriesAsync();
            var model = new ProductViewModel(categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model, int categoryId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productsServices.CreateProductAsync(_converter.ProductViewEntity(model, categoryId));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _productsServices.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(await _converter.ProductEntityView(product));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int categoryId, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsServices.EditProductAsync(_converter.ProductViewEntity(model, categoryId));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _productsServices.GetProductAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(await _converter.ProductEntityView(product));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productsServices.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        private bool ProductExists(int id)
        {
            return _productsServices.ProductExists(id);
        }
    }
}
