using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Purchases")]
    public class PurchasesController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public PurchasesController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Admin/Purchases
        public async Task<IActionResult> Index()
        {
            var purchases = await _context.Purchases
                .Include(p => p.Product)
                    .ThenInclude(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();

            foreach (var purchase in purchases)
            {
                if (purchase.Product != null && purchase.Product.Category != null)
                {
                    purchase.CategoryName = purchase.Product.Category.CategoryName;
                }
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            ViewBag.SourceAction = TempData["SourceAction"] as string;

            return View(purchases);
        }

        [Route("Details/{Id}")]
        // GET: Admin/Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        [Route("Create")]
        // GET: Admin/Purchases/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            return View();
        }

        [Route("Create")]
        // POST: Admin/Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,ProductId,SupplierId,PurchaseDate,Quantity,CostPrice,TotalCost")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Create Purchase Successful";
                TempData["SourceAction"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", purchase.SupplierId);

            TempData["ErrorMessage"] = "Create Purchase Failed";
            TempData["SourceAction"] = "Create";
            return View(purchase);
        }

        [Route("Edit/{Id}")]
        // GET: Admin/Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", purchase.SupplierId);
            return View(purchase);
        }

        [Route("Edit/{Id}")]
        // POST: Admin/Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,ProductId,SupplierId,PurchaseDate,Quantity,CostPrice,TotalCost")] Purchase purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    purchase.PurchaseDate = DateTime.Now;
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit Purchase Successful";
                    TempData["SourceAction"] = "Edit";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", purchase.SupplierId);

            TempData["ErrorMessage"] = "Edit Purchase Failed";
            TempData["SourceAction"] = "Edit";

            return View(purchase);
        }

        [Route("Delete/{Id}")]
        // GET: Admin/Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        [Route("Delete/{Id}")]
        // POST: Admin/Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'QLBHDbContext.Purchases'  is null.");
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Delete Purchase Successful";
            TempData["SourceAction"] = "Delete";

            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return (_context.Purchases?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }
    }
}
