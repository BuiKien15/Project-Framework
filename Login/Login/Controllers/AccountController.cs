using Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class AccountController : Controller
    {
        QuanLyBanHangDbContext _db = new QuanLyBanHangDbContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return View();
            else
            {
                return RedirectToAction("Home", "Index");
            }
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if(HttpContext.Session.GetString("UserName") == null) { 
                var u = _db.Users.Where(x=> x.UserName.Equals(user.UserName) && x.PassWord.Equals(user.PassWord)).FirstOrDefault();
                if(u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
