using P015Etut.Entities;

namespace P015Etut.Data
{
    public class Database
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }



        public Database() {

            Categories = new List<Category>() {
                new Category(){Id = 1, Name = "Bilgisayar"},
                new Category(){Id = 2, Name = "Telefon"},
                new Category(){Id = 3, Name = "Elektronik"},
                new Category(){Id = 4, Name = "Kulaklık"},
            };

            Products = new List<Product>() {
                new Product(){Id = 1, Name = "Asus bilgisayar", CategoryId= 1, Price = 15000, Stock = 10 },
                new Product(){Id = 2, Name = "Lenovo bilgisayar", CategoryId= 1, Price = 25000, Stock = 50 },
                new Product(){Id = 3, Name = "Iphone 14", CategoryId= 2, Price = 45000, Stock = 20 },
                new Product(){Id = 4, Name = "Samsung S20", CategoryId= 2, Price = 35000, Stock = 30 },
            
            };
        
        }
    }
}
