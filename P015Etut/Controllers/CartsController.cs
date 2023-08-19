using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P015Etut.Data;
using P015Etut.Entities;
using System.Security.Claims;

namespace P015Etut.Controllers
{
    public class CartsController : Controller
    {

        DatabaseContext database;

        public CartsController(DatabaseContext db)
        {
            database = db;
        }

        public IActionResult Index()
        {
            var user = database.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid)));

            var cart = database.Carts.FirstOrDefault(c => c.UserId == user.Id);

            var items = database.CartItems.Where(c => c.CartId == cart.Id).Include(c => c.Product);

            var totalPrice = items.Sum(c => c.Product.Price * c.Quantity);

            ViewBag.TotalPrice = totalPrice;
            return View(items.ToList());
        }

        public IActionResult List(int id)
        {
            var user = database.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid)));
            var carts = database.Carts.Where(c => c.UserId == user.Id).Include(c => c.CartItems).ThenInclude(c=>c.Product);

            return View(carts.ToList());
        }

        public IActionResult Details(int id)
        {
            return View();
        }


    }
}
