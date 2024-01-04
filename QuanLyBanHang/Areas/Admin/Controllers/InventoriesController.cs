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
    [Route("Admin/Inventories")]
    [Authorize(Roles = "0")]
    public class InventoriesController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public InventoriesController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Admin/Inventories
        public async Task<IActionResult> Index()
        {
            var inventoriesWithCategory = await _context.Inventories
                                        .Include(i => i.Product)
                                            .ThenInclude(p => p.Category)
                                        .ToListAsync();

            return View(inventoriesWithCategory);
        }

    }
}
