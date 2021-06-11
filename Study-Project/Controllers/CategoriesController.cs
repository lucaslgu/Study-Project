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
    public class CategoriesController : Controller
    {
        private readonly CategoriesService _categoriesServices;
        private readonly ProductsServices _productsServices;

        CategoryConverter converter = new CategoryConverter();

        public CategoriesController(CategoriesService categoriesServices, ProductsServices productsServices)
        {
            _categoriesServices = categoriesServices;
            _productsServices = productsServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoriesServices.GetCategoriesAsync();
                var models = new List<CategoryViewModel>();

                foreach (var category in categories)
                {
                    models.Add(converter.CategoryEntityView(category));
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
                var category = await _categoriesServices.GetCategoryAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(converter.CategoryEntityView(category));
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
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoriesServices.CreateCategoryAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!CategoryExists(id))
            {
                return NotFound();
            }

            try
            {
                var category = await _categoriesServices.FindCategoryAsync(id);

                return View(category);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriesServices.EditCategoryAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!CategoryExists(id))
            {
                return NotFound();
            }

            try
            {
                var category = await _categoriesServices.FindCategoryAsync(id);
                var producs = await _productsServices.GetProductsForCategoryAsync(id);

                if(producs.Count > 0)
                    return View(category);

                return View();
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
                await _categoriesServices.DeleteCategoryAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        private bool CategoryExists(int? id)
        {
            return _categoriesServices.CategoryExists(id);
        }
    }
}
