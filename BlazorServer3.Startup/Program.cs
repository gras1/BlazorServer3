using BlazorServer3.DataAccess;
using BlazorServer3.Database;
using System;

#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA1031 // Do not catch general exception types
namespace BlazorServer3.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IDbContext db = new MyDbContext();

                db.RecreateDatabase();

                ICategoryData categoryData = new CategoryData(db);
                IProductData productData = new ProductData(db);

                Console.WriteLine("Creating database entries");

                categoryData.AddCategory(new ClassLibrary.Category { CategoryId = 1, Name = "Fruits" });
                categoryData.AddCategory(new ClassLibrary.Category { CategoryId = 2, Name = "Vegetables" });
                categoryData.AddCategory(new ClassLibrary.Category { CategoryId = 3, Name = "Pulses" });

                productData.AddProduct(new ClassLibrary.Product { ProductId = 1, Name = "Orange", Price = 0.45m, ImageFilePath = "https://upload.wikimedia.org/wikipedia/commons/c/c4/Orange-Fruit-Pieces.jpg", CategoryId = 1 });
                productData.AddProduct(new ClassLibrary.Product { ProductId = 2, Name = "Apple", Price = 0.35m, ImageFilePath = "https://cdn.ecommercedns.uk/files/1/231541/9/8022859/green20apple.jpg", CategoryId = 1 });
                productData.AddProduct(new ClassLibrary.Product { ProductId = 3, Name = "Banana", Price = 0.65m, ImageFilePath = "https://api.time.com/wp-content/uploads/2019/11/gettyimages-459761948.jpg?w=800&quality=85", CategoryId = 1 });
                productData.AddProduct(new ClassLibrary.Product { ProductId = 4, Name = "Cabbage", Price = 0.85m, ImageFilePath = "https://cdn-prod.medicalnewstoday.com/content/images/articles/284/284823/one-big-cabbage.jpg", CategoryId = 2 });
                productData.AddProduct(new ClassLibrary.Product { ProductId = 5, Name = "Pepper", Price = 0.40m, ImageFilePath = "http://www.lima-europe.eu/wp-content/uploads/2017/04/pepper.jpg", CategoryId = 2 });
                productData.AddProduct(new ClassLibrary.Product { ProductId = 6, Name = "Red Lentil", Price = 2.10m, ImageFilePath = "https://upload.wikimedia.org/wikipedia/commons/4/43/Split_Red_Lentil.jpg", CategoryId = 3 });

                Console.WriteLine("Database entries created");

                foreach (var product in productData.GetProducts())
                {
                    Console.WriteLine($"ProductId={product.ProductId}\tName={product.Name}\tPrice={product.Price}\tImageFilePath={product.ImageFilePath}\tCategoryId={product.CategoryId}");
                }
                foreach (var category in categoryData.GetCategories())
                {
                    Console.WriteLine($"CategoryId={category.CategoryId}\tName={category.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
