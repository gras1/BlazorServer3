using BlazorServer3.Database;
using System.Collections.Generic;
using System.Linq;

namespace BlazorServer3.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly IDbContext _dbContext;

        public ProductData(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddProduct(ClassLibrary.Product product)
        {
            _dbContext.Products.Add(new Product {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                ImageFilePath = product.ImageFilePath,
                CategoryId = product.CategoryId
            });
            _dbContext.SaveChanges();
        }

        public List<ClassLibrary.Product> GetProducts()
        {
            return (from p in _dbContext.Products
                   where p.ProductId > 0
                   select new ClassLibrary.Product
                   {
                       ProductId = p.ProductId,
                       Name = p.Name,
                       Price = p.Price,
                       ImageFilePath = p.ImageFilePath,
                       CategoryId = p.CategoryId
                   }).ToList();
        }

        ~ProductData()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
