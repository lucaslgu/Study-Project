using Microsoft.EntityFrameworkCore;
using Study_Project.Data;
using Study_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Services
{
    public class ExpensesServices
    {
        private readonly ApplicationDbContext _context;

        public ExpensesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> GetExpenseAsync(int? id)
        {
            return await _context.Expenses
                        .Include(p => p.Categories)
                        .Include(e => e.Products)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateExpenseAsync(Expense expense)
        {
            _context.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task EditExpenseAsync(Expense expense)
        {
            _context.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<Expense> FindExpenseAsync(int? id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public bool ExpenseExists(int? id)
        {
            return _context.Expenses.Any(e => e.Id == id.Value);
        }

        public async Task<List<Expense>> GetExpenseForCategoryAsync(int? idCategory)
        {
            return await _context.Expenses.Include(e => e.Categories.Where(c => c.Id == idCategory.Value)).ToListAsync();
        }

        public async Task<List<Expense>> GetExpenseForProductAsync(int? idProduct)
        {
            return await _context.Expenses.Include(e => e.Products.Where(p => p.Id == idProduct.Value)).ToListAsync();
        }
    }
}
