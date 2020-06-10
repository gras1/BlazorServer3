using BlazorServer3.ClassLibrary;
using BlazorServer3.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer3.DataAccess
{
    public class ProductCategoryData : IProductCategoryData
    {
        private readonly IDbContext _dbContext;

        public ProductCategoryData(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<ProductCategory>> GetAllAsync()
        {
            var categoriesTask = _dbContext.Categories.ToArrayAsync();
            var productsTask = _dbContext.Products.ToArrayAsync();
            await Task.WhenAll(categoriesTask, productsTask);

            Database.Product[] products = productsTask.Result;
            Database.Category[] categories = categoriesTask.Result;
            ProductCategory[] productCategories = new ProductCategory[products.Length];
            var counter = 0;
            foreach (var product in products)
            {
                productCategories[counter] = new ProductCategory
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    ImageFilePath = product.ImageFilePath,
                    CategoryName = (from c in categories where c.CategoryId == product.CategoryId select c.Name).FirstOrDefault()
                };
                counter++;
            }
            return productCategories;
        }

        public async Task<ICollection<ProductCategory>> SearchProductsAsync(string searchTerm)
        {
            var categoriesTask = _dbContext.Categories.ToArrayAsync();
            var productsTask = _dbContext.Products.ToArrayAsync();
            await Task.WhenAll(categoriesTask, productsTask);

            Database.Product[] products = productsTask.Result;
            var searchedProducts = productsTask.Result.Where(p => p.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
            Database.Category[] categories = categoriesTask.Result;
            var searchedCategories = categoriesTask.Result.Where(c => c.Name.ToLower().Contains(searchTerm.Trim().ToLower()));

            List<ProductCategory> productCategories = new List<ProductCategory>();

            foreach (var searchedProduct in searchedProducts)
            {
                productCategories.Add(new ProductCategory
                {
                    ProductId = searchedProduct.ProductId,
                    Name = searchedProduct.Name,
                    Price = searchedProduct.Price,
                    ImageFilePath = searchedProduct.ImageFilePath,
                    CategoryName = (from c in categories where c.CategoryId == searchedProduct.CategoryId select c.Name).FirstOrDefault()
                });
            }

            foreach (var searchedCategory in searchedCategories)
            {
                var searchedCategoryProducts = products.Where(p => p.CategoryId == searchedCategory.CategoryId);

                foreach (var searchedCategoryProduct in searchedCategoryProducts)
                {
                    if (productCategories.Count(pc => pc.ProductId == searchedCategoryProduct.ProductId) == 0)
                    {
                        productCategories.Add(new ProductCategory
                        {
                            ProductId = searchedCategoryProduct.ProductId,
                            Name = searchedCategoryProduct.Name,
                            Price = searchedCategoryProduct.Price,
                            ImageFilePath = searchedCategoryProduct.ImageFilePath,
                            CategoryName = (from c in categories where c.CategoryId == searchedCategoryProduct.CategoryId select c.Name).FirstOrDefault()
                        });
                    }
                }
            }

            return productCategories.ToArray();
        }

        ~ProductCategoryData()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
