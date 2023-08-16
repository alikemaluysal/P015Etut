using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P015Etut.Data;
using P015Etut.Models;
using System.Xml.Linq;

namespace P015Etut.Controllers
{
    public class ProductsController : Controller
    {
        //Database database = new Database();

        //DatabaseContext database = new DatabaseContext();

        DatabaseContext database;

        public ProductsController(DatabaseContext db)
        {
            database = db;
        }



        public async Task<IActionResult> Index(
            int? id,
            string? search,
            int? pageNumber,
            string? sortOrder
            )
        {
            var products = database.Products.AsQueryable();
            ViewBag.Title = "";

            ViewData["CurrentSort"] = sortOrder;


            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "price";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";




            if (id.HasValue)
            {
                products = database.Products.Where(p => p.CategoryId == id);
                ViewBag.Title = database.Categories.FirstOrDefault(c => c.Id == id).Name;
            }

            if (search != null)
            {
                ViewBag.Title = search;
                products = database.Products.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case "price":
                    products = products.OrderBy(s => s.Price);
                    break;
            }

            int pageSize = 10;
            var list = await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize);


            return View(list);
        }



        public IActionResult Details(int id)
        {
            var product = database.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}
