﻿using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetUserProductsAsync(int userId, ProductParameters productParameters);
        Task DeleteProductAsync(int productId, int userId);
        Task<ProductDto> CreateProductAsync(int userId, ProductCreationDto productForCreationDto);
        Task<ProductDto> SellProductAsync(int userId, int productId);
        Task UpdateProductAsync(int userId, int productId, ProductUpdateDto productForUpdateDto);
        Task<IEnumerable<ProductImageDto>> AddPhotosAsync(int userId, int productId, IFormFileCollection? images);
        Task<IEnumerable<ProductImageDto>> GetProductPhotosAsync(int productId);
        Task DeleteProductPhotosAsync(int userId, int productId);
    }
}
