using Microsoft.EntityFrameworkCore;
using Study_Project.Data;
using Study_Project.Models;
using Study_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Services
{
    public class CategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int? id)
        {
            return await _context.Categories
                        .Include(p => p.Products)
                        .Include(e => e.Expenses)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateCategoryAsync(CategoryViewModel model)
        {
            var category = new Category(model.Id, model.Name);
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditCategoryAsync(CategoryViewModel model)
        {
            var category = new Category(model.Id, model.Name);
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> FindCategoryAsync(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public bool CategoryExists(int? id)
        {
            return _context.Categories.Any(e => e.Id == id.Value);
        }
    }
}
