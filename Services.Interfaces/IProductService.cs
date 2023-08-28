using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetUserProductsAsync(int userId);
        Task DeleteProductAsync(int productId);

        Task<ProductDto> CreateProductAsync(int userId, ProductForCreationDto productForCreationDto);
        Task<ProductDto> SellProductAsync(int userId, int productId);
    }
}
