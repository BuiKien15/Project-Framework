using k.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;
using X.PagedList;

namespace k.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangContext db=new QuanLyBanHangContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber=page==null ||page<0?1:page.Value;
            var dssanpham = db.Products.AsNoTracking().OrderBy(x=>x.ProductId);
            PagedList<Product> ds=new PagedList<Product>(dssanpham,pageNumber,pageSize);
            return View(ds);
        }

        public IActionResult SanPhamTheoLoai(int category_id,int? page)
        {
            /*List<Product> dssanpham=db.Products.Where(x=>x.CategoryId==category_id).OrderBy(x=>x.ProductName).ToList();*/
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var dssanpham = db.Products.AsNoTracking().Where(x=>x.CategoryId==category_id).OrderBy(x => x.ProductId);
            PagedList<Product> ds = new PagedList<Product>(dssanpham, pageNumber, pageSize);
            return View(ds);
        }
        public IActionResult ChiTietSanPham(int product_id)
        {
            var sanPham = db.Products.SingleOrDefault(x => x.ProductId == product_id);
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