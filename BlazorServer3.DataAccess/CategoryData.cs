using BlazorServer3.Database;
using System.Collections.Generic;
using System.Linq;

namespace BlazorServer3.DataAccess
{
    public class CategoryData : ICategoryData
    {
        private readonly IDbContext _dbContext;

        public CategoryData(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCategory(ClassLibrary.Category category)
        {
            _dbContext.Categories.Add(new Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            });
            _dbContext.SaveChanges();
        }

        public List<ClassLibrary.Category> GetCategories()
        {
            return (from c in _dbContext.Categories
                    where c.CategoryId > 0
                    select new ClassLibrary.Category
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name
                    }).ToList();
        }

        ~CategoryData()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
