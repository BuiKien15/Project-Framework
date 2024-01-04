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
    [Route("Admin/Orders")]
    [Authorize(Roles = "0")]
    public class OrdersController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public OrdersController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var quanLyBanHangDbContext = _context.Orders.Include(o => o.User);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        [Route("Details/{Id}")]
        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderItems = await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == id)
                .ToListAsync();

            return View(order);
        }

        // Trong controller OrdersController.cs
        [HttpPost]
        [Route("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Thực hiện cập nhật Status theo logic của bạn (ví dụ: Processing -> Shipping -> Completed)
            if (order.Status == "Processing")
            {
                order.Status = "Shipping";
            }
            else if (order.Status == "Shipping")
            {
                order.Status = "Completed";
            }

            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return Ok(); // Trả về OK nếu cập nhật thành công
        }
    }
}
