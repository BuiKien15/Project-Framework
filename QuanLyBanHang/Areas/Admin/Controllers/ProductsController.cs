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
    [Route("Admin/Products")]
    public class ProductsController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(QuanLyBanHangDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [Route("")]
        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            ViewBag.SourceAction = TempData["SourceAction"] as string;

            var qLBHDbContext = _context.Products.Include(p => p.Category);
            return View(await qLBHDbContext.ToListAsync());
        }

        [Route("Details/{Id}")]
        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet("IsProductNameUnique")]
        public IActionResult IsProductNameUnique(string productName, int productId)
        {
            var isUnique = !_context.Products.Any(p => p.ProductName == productName && p.ProductId != productId);
            return Json(isUnique);
        }

        [Route("Create")]
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadImage(product);
                    var data = new Product()
                    {
                        ProductName = product.ProductName,
                        CostPrice = product.CostPrice,
                        SellingPrice = product.SellingPrice,
                        Description = product.Description,
                        ImageUrl = uniqueFileName,
                        CategoryId = product.CategoryId,
                    };
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Create Product Successful";
                    TempData["SourceAction"] = "Create";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Create Product Failed";
                TempData["SourceAction"] = "Create";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        private string UploadImage(Product product)
        {
            string uniqueFileName = string.Empty;
            if (product.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Route("Edit/{Id}")]
        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{Id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _context.Products.FindAsync(product.ProductId);
                    string uniqueFileName = string.Empty;
                    // Check if a new image is uploaded
                    if (product.ImagePath != null)
                    {
                        if (data.ImageUrl != null)
                        {
                            // Delete the existing image file
                            string filePath = Path.Combine(_environment.WebRootPath, "images/", data.ImageUrl);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                        uniqueFileName = UploadImage(product);
                    }

                    data.ProductName = product.ProductName;
                    data.CostPrice = product.CostPrice;
                    data.SellingPrice = product.SellingPrice;
                    data.Description = product.Description;
                    data.CategoryId = product.CategoryId;
                    if (product.ImagePath != null)
                    {
                        data.ImageUrl = uniqueFileName;
                    }
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit Product Successful";
                    TempData["SourceAction"] = "Edit";
                }
                else
                {
                    TempData["ErrorMessage"] = "Edit Product Failed";
                    TempData["SourceAction"] = "Edit";
                    return View(product);
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToActionPermanent("Index");
        }

        // GET: Admin/Products/Delete/5
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [Route("Delete/{Id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Products == null)
            {
                return Problem("Entity set 'QLBHDbContext.Products' is null.");
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                string deleteFromFolder = Path.Combine(_environment.WebRootPath, "images/");
                string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, product.ImageUrl);
                if (currentImage != null)
                {
                    if (System.IO.File.Exists(currentImage))
                    {
                        // Gọi stored procedure và nhận giá trị trả về từ Value Tuple
                        var (result, errorMessage) = await _context.DeleteProductWithCheckAsync(id);

                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            // Xử lý thông báo lỗi ở đây
                            TempData["ErrorMessage"] = errorMessage;
                            TempData["SourceAction"] = "Delete";
                            return RedirectToAction(nameof(Index));
                        }

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _context.SaveChangesAsync();

                        // Xóa ảnh sau khi đã xác nhận xóa thành công từ stored procedure
                        System.IO.File.Delete(currentImage);

                        TempData["SuccessMessage"] = "Delete Product Successful";
                        TempData["SourceAction"] = "Delete";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            TempData["ErrorMessage"] = "Delete Product Failed";
            TempData["SourceAction"] = "Delete";
            return RedirectToAction(nameof(Index));

        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
