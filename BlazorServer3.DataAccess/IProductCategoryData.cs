using BlazorServer3.ClassLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer3.DataAccess
{
    public interface IProductCategoryData
    {
        Task<ICollection<ProductCategory>> GetAllAsync();

        Task<ICollection<ProductCategory>> SearchProductsAsync(string searchTerm);
    }
}