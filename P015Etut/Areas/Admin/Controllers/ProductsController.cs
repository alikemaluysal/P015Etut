using Microsoft.AspNetCore.Mvc;
using P015Etut.Data;

namespace P015Etut.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
