using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class RegisterController : Controller
    {
        private readonly QuanLyBanHangDbContext _db;

        public RegisterController(QuanLyBanHangDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string UserName, string Email,string Address, string Name, string Phone, string PassWord)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = UserName,
                    Email = Email,
                    Address = Address,
                    Name = Name,
                    Phone = Phone,
                    PassWord = PassWord
                };

                var result = _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
