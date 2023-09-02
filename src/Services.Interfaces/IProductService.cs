﻿using Shared.DataTransferObjects;
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
        Task<ProductDto> UpdateProductAsync(int userId, ProductForUpdateDto productForUpdateDto);
        Task AddPhotosAsync(int userId, int productId, IFormFileCollection? images);
    }
}