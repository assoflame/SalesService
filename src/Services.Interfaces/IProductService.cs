using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetUserProductsAsync(int userId, ProductParameters productParameters);
        Task DeleteProductAsync(int productId);
        Task<ProductDto> CreateProductAsync(int userId, ProductForCreationDto productForCreationDto,
            IFormFileCollection? images = null);
        Task<ProductDto> SellProductAsync(int userId, int productId);
        Task UpdateProductAsync(int userId, int productId, ProductForUpdateDto productForUpdateDto);
        Task<IEnumerable<ProductImageDto>> AddPhotosAsync(int userId, int productId, IFormFileCollection? images);
        Task<IEnumerable<ProductImageDto>> GetProductPhotos(int productId);
        Task DeleteProductPhotos(int userId, int productId);
    }
}
