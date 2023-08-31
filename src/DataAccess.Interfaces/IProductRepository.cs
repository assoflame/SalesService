using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product?> GetProductByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Product>> GetUserProductsAsync(int userId, bool trackChanges);
        Task<Product?> GetUserProductAsync(int userId, int productId, bool trackChanges);
        void Delete(Product product);
        void Create(Product product);
        void Update(Product product);
    }
}
