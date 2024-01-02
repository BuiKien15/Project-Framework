using Microsoft.AspNetCore.Mvc;
using k.Models;
using k.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using k.Models.ViewModel;

namespace k.Controllers
{
    public class CartController:Controller
    {
        private readonly QuanLyBanHangContext _datacontext;
        public CartController(QuanLyBanHangContext _context)
        {
            _datacontext = _context;
        }
        public IActionResult Index()
        {
            List<CartItemModel>cartItems=HttpContext.Session.GetJson<List<CartItemModel>>("Cart")??new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public async Task<IActionResult>Add(int Id)
        {
            Product product = await _datacontext.Products.FindAsync(Id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems=cart.Where(c=>c.ProductId == Id).FirstOrDefault();
            if(cartItems==null)
            {
                cart.Add(new CartItemModel (product));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
