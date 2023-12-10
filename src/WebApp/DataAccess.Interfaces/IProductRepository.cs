using SalesService.Entities.Models;
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
