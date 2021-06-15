using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Project.Data;
using Study_Project.Models;
using Study_Project.Services;

namespace Study_Project.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesServices _expensesServices;

        public ExpensesController(ExpensesServices expensesServices)
        {
            _expensesServices = expensesServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _expensesServices.GetAllExpensesAsync());
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
                var expense = await _expensesServices.GetExpenseAsync(id);

                if (expense == null)
                {
                    return NotFound();
                }

                return View(expense);
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
        public async Task<IActionResult> Create(Expense expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _expensesServices.CreateExpenseAsync(expense);
                    return RedirectToAction(nameof(Index));
                }
                return View(expense);
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

            try
            {
                var expense = await _expensesServices.GetExpenseAsync(id);

                if (expense == null)
                {
                    return NotFound();
                }
                return View(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _expensesServices.EditExpenseAsync(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
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
            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var expense = await _expensesServices.GetExpenseAsync(id);

                if (expense == null)
                {
                    return NotFound();
                }

                return View(expense);
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
                await _expensesServices.DeleteExpenseAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        private bool ExpenseExists(int id)
        {
            return _expensesServices.ExpenseExists(id);
        }
    }
}
