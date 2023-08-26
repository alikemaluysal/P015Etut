using Bogus;
using Microsoft.EntityFrameworkCore;
using App.Data.Entities;
using System.Xml.Linq;

namespace App.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var categoryIds = 1;
            var categoryFaker = new Faker<Category>()
                .RuleFor(p => p.Id, f => categoryIds++)
                .RuleFor(p => p.Name, f => f.Commerce.Categories(1).First());

            var categorySeeds = categoryFaker.Generate(5);

            modelBuilder.Entity<Category>().HasData(categorySeeds);

            var productIds = 1;
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => productIds++)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Stock, f => f.Random.Int(10, 500))
                .RuleFor(p => p.Price, f => f.Random.Int(10, 1000))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categorySeeds).Id);
            //.RuleFor(p => p.CategoryId, f => f.Random.Int(1, 4));


            var productSeeds = productFaker.Generate(200);

            modelBuilder.Entity<Product>().HasData(productSeeds);

            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Ali Kemal" ,Surname="Uysal", Email="alikemal.uysal@siliconmade.com", Password = "1234"},
                new User() { Id = 2, Name = "Furkan" ,Surname="Üçgül", Email="furkan@siliconmade.com", Password = "4545"}

                );


            //modelBuilder.Entity<Category>().HasData(
            //    new Category() { Id = 1, Name = "Bilgisayar" },
            //    new Category() { Id = 2, Name = "Telefon" },
            //    new Category() { Id = 3, Name = "Elektronik" },
            //    new Category() { Id = 4, Name = "Kulaklık" }
            //    );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product() { Id = 1, Name = "Asus bilgisayar", CategoryId = 1, Price = 15000, Stock = 10 },
            //    new Product() { Id = 2, Name = "Lenovo bilgisayar", CategoryId = 1, Price = 25000, Stock = 50 },
            //    new Product() { Id = 3, Name = "Iphone 14", CategoryId = 2, Price = 45000, Stock = 20 },
            //    new Product() { Id = 4, Name = "Samsung S20", CategoryId = 2, Price = 35000, Stock = 30 }
            //    );





        }
    }
}
