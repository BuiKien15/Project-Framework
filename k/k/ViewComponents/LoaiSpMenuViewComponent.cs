using k.Models;
using k.Repository;
using Microsoft.AspNetCore.Mvc;
namespace k.ViewComponents
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
