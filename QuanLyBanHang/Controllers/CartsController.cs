using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class CartsController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public CartsController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = "";

            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }

            var quanLyBanHangDbContext = _context.Carts.Where(c => c.UserId == int.Parse(userId)).Include(c => c.Product);

            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound(); // Xử lý trường hợp sản phẩm không tồn tại
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = "";
            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var existingCartItem = _context.Carts
                .Where(c => c.UserId == int.Parse(userId) && c.ProductId == productId)
                .FirstOrDefault();

            if (existingCartItem != null)
            {
                // Nếu sản phẩm đã tồn tại trong giỏ hàng, cập nhật số lượng
                existingCartItem.Quantity += 1;
                existingCartItem.TotalPrice = existingCartItem.SellingPrice * existingCartItem.Quantity;
            }
            else
            {
                // Nếu sản phẩm chưa có trong giỏ hàng, tạo một mục mới
                var newCartItem = new Cart
                {
                    UserId = int.Parse(userId),
                    ProductId = productId,
                    Quantity = 1,
                    SellingPrice = product.SellingPrice,
                };

                newCartItem.TotalPrice = newCartItem.SellingPrice * newCartItem.Quantity;

                _context.Carts.Add(newCartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Carts");
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId,ProductId,Quantity,SellingPrice,TotalPrice")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cart.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cart.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId,ProductId,Quantity,SellingPrice,TotalPrice")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cart.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'QuanLyBanHangDbContext.Carts'  is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return (_context.Carts?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
