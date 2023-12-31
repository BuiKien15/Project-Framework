using QuanLyBanHang.Models;
using QuanLyBanHang.Repository;
using Microsoft.AspNetCore.Mvc;
namespace QuanLyBanHang.ViewComponents
{
    public class LoaiSpMenuViewComponent:ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;

        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetAllCategories().OrderBy(x => x.CategoryId);
            return View(loaisp);
        }
    }
}
