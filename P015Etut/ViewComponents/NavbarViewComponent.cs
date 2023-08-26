using Microsoft.AspNetCore.Mvc;
using App.Data.Context;
using P015Etut.Models;

namespace P015Etut.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        //Kategori bilgilerini almak

        //Veri tabanına bağlan
        //Kategoriler tablosunu çek
        //Onları view'a yolla


        //Gerçek
        DatabaseContext database;

        public NavbarViewComponent(DatabaseContext database)
        {
            this.database = database;
        }

 

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Simülasyon
            //var database = new Database();

            var categories = database.Categories.ToList();

            return View(categories);
        }
    }
}
