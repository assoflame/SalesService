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
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges);
        Task<Product?> GetProductByIdAsync(int id, bool trackChanges);
        Task<PagedList<Product>> GetUserProductsAsync(
            int userId, ProductParameters productParameters, bool trackChanges);
        Task DeleteUserProducts(int userId);
        Task<Product?> GetUserProductAsync(int userId, int productId, bool trackChanges);
    }
}
