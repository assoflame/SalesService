using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters);
        Task<Product?> GetProductByIdAsync(int id);
        Task<PagedList<Product>> GetUserProductsAsync(int userId, ProductParameters productParameters);
        Task DeleteUserProducts(int userId);
        Task<Product?> GetUserProductAsync(int userId, int productId);
    }
}
