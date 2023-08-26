using Microsoft.AspNetCore.Mvc;
using App.Data.Context;
using App.Data.Entities;
using App.Data.Concrete;
using App.Data.Abstract;

namespace P015Etut.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            var categories = _repository.Get();
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
                _repository.Create(category);
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            if (id == null || _repository.Get() == null)
            {
                return NotFound();
            }

            var category = _repository.GetById(id);

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
                    _repository.Update(category.Id, category);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == null || _repository.Get() == null)
            {
                return NotFound();
            }

            var category = _repository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            var categoryToDelete = _repository.GetById(category.Id);


            if (categoryToDelete == null)
            {

                return NotFound();
            }

            _repository.Delete(categoryToDelete.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
