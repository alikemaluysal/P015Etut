using Microsoft.AspNetCore.Mvc;
using P015Etut.Data;
using P015Etut.Models;

namespace P015Etut.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();

            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(c => c.Id == category.Id);


            if (categoryToDelete == null)
            {

                return NotFound();
            }

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
