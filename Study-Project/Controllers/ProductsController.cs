using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Project.Data;
using Study_Project.Models;
using Study_Project.Models.Converters;
using Study_Project.Models.ViewModels;
using Study_Project.Services;

namespace Study_Project.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductsServices _productsServices;
        ProductsConverter converter = new ProductsConverter();

        public ProductsController(ProductsServices productsServices)
        {
            _productsServices = productsServices;
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
                    models.Add(converter.ProductEntityView(product));
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

                return View(converter.ProductEntityView(product));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productsServices.CreateProductAsync(converter.ProductViewEntity(model));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _productsServices.FindProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(converter.ProductEntityView(product));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsServices.EditProductAsync(converter.ProductViewEntity(model));
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
                var product = await _productsServices.FindProductAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(converter.ProductEntityView(product));
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
