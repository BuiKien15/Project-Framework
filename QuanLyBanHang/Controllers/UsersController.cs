using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class UsersController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public UsersController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = "";

            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }

            var user = _context.Users.SingleOrDefault(o => o.UserId == int.Parse(userId));

            if (user == null || _context.Users == null)
            {
                return View();
            }

            return View(user);

        }

        [HttpGet("IsUserNameUnique")]
        public IActionResult IsUserNameUnique(string userName, int userId)
        {
            var isUnique = !_context.Users.Any(u => u.UserName == userName && u.UserId != userId);
            return Json(isUnique);
        }

        [HttpGet("IsPhoneUnique")]
        public IActionResult IsPhoneUnique(string phone, int userId)
        {
            var isUnique = !_context.Users.Any(u => u.Phone == phone && u.UserId != userId);
            return Json(isUnique);
        }

        [HttpGet("IsEmailUnique")]
        public IActionResult IsEmailUnique(string email, int userId)
        {
            var isUnique = !_context.Users.Any(u => u.Email == email && u.UserId != userId);
            return Json(isUnique);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Login()
        {

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            var user = _context.Users.Where(u => u.UserName == userName && u.PassWord == passWord).FirstOrDefault<User>();

            if (user == null || _context.Users == null)
            {
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = 1;

                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tạo tài khoản thành công!";

                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Email,Address,Phone,UserName,PassWord,Role")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'QuanLyBanHangDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
