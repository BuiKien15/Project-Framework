using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList;

namespace QuanLyBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangDbContext db = new QuanLyBanHangDbContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var dssanpham = db.Products.AsNoTracking().OrderBy(x => x.ProductId).Include(x => x.Inventory);
            PagedList<Product> ds = new PagedList<Product>(dssanpham, pageNumber, pageSize);

            return View(ds);
        }

        public IActionResult SanPhamTheoLoai(int category_id, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var dssanpham = db.Products.AsNoTracking().Where(x => x.CategoryId == category_id).OrderBy(x => x.ProductId).Include(x => x.Inventory);
            PagedList<Product> ds = new PagedList<Product>(dssanpham, pageNumber, pageSize);

            var category = db.Categories.SingleOrDefault(c => c.CategoryId == category_id);
            ViewBag.CategoryName = category.CategoryName;

            return View(ds);
        }

        public async Task<IActionResult> ChiTietSanPham(int? id)
        {
            var sanPham = db.Products.SingleOrDefault(x => x.ProductId == id);

            var inventory = db.Inventories.SingleOrDefault(x => x.ProductId == id);

            ViewBag.Quantity = inventory.Quantity;

            return View(sanPham);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}