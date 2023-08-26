using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using App.Data.Context;
using P015Etut.Models;
using System.Security.Claims;

namespace P015Etut.Controllers
{
    public class AuthController : Controller
    {

        DatabaseContext database;

        public AuthController(DatabaseContext db)
        {
            database = db;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = $"{User.FindFirstValue(ClaimTypes.Name)} -- {User.FindFirstValue(ClaimTypes.PrimarySid)}";
            return View();
        }

        [HttpGet("Login")]

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]LoginViewModel login, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = database.Users.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                ViewBag.Error = "Email adresi veya şifre hatalı";
                return View(user);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };


            await HttpContext.SignInAsync(principal, properties);

            if (returnUrl is not null)
            {
                return LocalRedirect(returnUrl);
            }

            return View("Index", "Home");
        }

        [HttpGet("Logout")]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
