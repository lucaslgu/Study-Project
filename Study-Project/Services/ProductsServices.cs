using Microsoft.EntityFrameworkCore;
using Study_Project.Data;
using Study_Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Services
{
    public class ProductsServices
    {
        private readonly ApplicationDbContext _context;

        public ProductsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductAsync(int? id)
        {
            return await _context.Products
                        .Include(p => p.Category)
                        .Include(e => e.Expenses)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateProductAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public bool ProductExists(int? id)
        {
            return _context.Products.Any(e => e.Id == id.Value);
        }

        public async Task<List<Product>> GetProductsForCategoryAsync(int? idCategory)
        {
            return await _context.Products.Where(p => p.CategoryId == idCategory.Value).ToListAsync();
        }
    }
}
