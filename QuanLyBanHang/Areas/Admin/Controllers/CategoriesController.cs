using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Categories")]
    [Authorize(Roles = "0")]
    public class CategoriesController : Controller
    {
        
        private readonly QuanLyBanHangDbContext _context;

        public CategoriesController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            ViewBag.SourceAction = TempData["SourceAction"] as string;

            var categories = await _context.Categories.ToListAsync();

            foreach (var category in categories)
            {
                category.ProductCount = _context.Products.Count(p => p.CategoryId == category.CategoryId);
            }

            return categories != null ?
                   View(categories) :
                   Problem("Entity set 'QuanLyBanHangDbContext.Categories'  is null.");

        }

        [Route("Details/{Id}")]
        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet("IsCategoryNameUnique")]
        public IActionResult IsCategoryNameUnique(string categoryName, int categoryId)
        {
            var isUnique = !_context.Categories.Any(c => c.CategoryName == categoryName && c.CategoryId != categoryId);
            return Json(isUnique);
        }


        [Route("Create")]
        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Create Category Successful";
                TempData["SourceAction"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Create Category Failed";
            TempData["SourceAction"] = "Create";
            return View(category);
        }

        [Route("Edit/{Id}")]
        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{Id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit Category Successful";
                    TempData["SourceAction"] = "Edit";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        TempData["ErrorMessage"] = "Category NotFound!";
                        TempData["SourceAction"] = "Edit";
                        return NotFound();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Edit Category Failed";
                        TempData["SourceAction"] = "Edit";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Route("Delete/{Id}")]
        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [Route("Delete/{Id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'QuanLyBanHangDbContext.Categories'  is null.");
            }

            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category != null)
                {
                    _context.Categories.Remove(category);
                }
                
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Delete Category Successful";
                TempData["SourceAction"] = "Delete";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Delete Category Failed";
                TempData["SourceAction"] = "Delete";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
