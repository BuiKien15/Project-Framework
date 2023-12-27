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
    [Route("Admin/Suppliers")]
    public class SuppliersController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public SuppliersController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Admin/Suppliers
        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            ViewBag.SourceAction = TempData["SourceAction"] as string;

            var suppliers = await _context.Suppliers.Include(s => s.Purchases).ToListAsync();

            // Tính toán số lượng đơn hàng đã bán và gán vào thuộc tính NumberOfPurchases của từng Supplier
            foreach (var supplier in suppliers)
            {
                supplier.NumberOfPurchases = supplier.Purchases.Count;
            }

            return suppliers != null ?
                View(suppliers) :
                Problem("Entity set 'QLBHDbContext.Suppliers' is null.");

        }

        [Route("Details/{Id}")]
        // GET: Admin/Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpGet("IsSupplierNameUnique")]
        public IActionResult IsSupplierNameUnique(string supplierName, int supplierId)
        {
            var isUnique = !_context.Suppliers.Any(s => s.SupplierName == supplierName && s.SupplierId != supplierId);
            return Json(isUnique);
        }

        [HttpGet("IsPhoneUnique")]
        public IActionResult IsPhoneUnique(string phone, int supplierId)
        {
            var isUnique = !_context.Suppliers.Any(s => s.Phone == phone && s.SupplierId != supplierId);
            return Json(isUnique);
        }

        [HttpGet("IsEmailUnique")]
        public IActionResult IsEmailUnique(string email, int supplierId)
        {
            var isUnique = !_context.Suppliers.Any(s => s.Email == email && s.SupplierId != supplierId);
            return Json(isUnique);
        }

        [Route("Create")]
        // GET: Admin/Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,ContactName,Phone,Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Create Supplier Successful";
                TempData["SourceAction"] = "Create";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Create Supplier Failed";
            TempData["SourceAction"] = "Create";
            return View(supplier);
        }

        [Route("Edit/{Id}")]
        // GET: Admin/Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{Id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,ContactName,Phone,Email")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit Supplier Successful";
                    TempData["SourceAction"] = "Edit";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
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
            TempData["ErrorMessage"] = "Edit Supplier Failed";
            TempData["SourceAction"] = "Edit";
            return View(supplier);
        }

        [Route("Delete/{Id}")]
        // GET: Admin/Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [Route("Delete/{Id}")]
        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Suppliers == null)
                {
                    return Problem("Entity set 'QLBHDbContext.Suppliers'  is null.");
                }
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier != null)
                {
                    _context.Suppliers.Remove(supplier);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Delete Supplier Successful";
                TempData["SourceAction"] = "Delete";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Delete Supplier Failed";
                TempData["SourceAction"] = "Delete";
                return RedirectToAction(nameof(Index));
            }
            
        }

        private bool SupplierExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
