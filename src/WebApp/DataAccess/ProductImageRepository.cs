﻿using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;

namespace DataAccess
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationContext context) : base(context) { }

        public void CreateRange(IEnumerable<ProductImage> productImages)
            => dbContext.AddRange(productImages);

        public async Task<IEnumerable<ProductImage>> GetProductPhotosAsync(int productId)
        {
            var photos = await dbContext.ProductImages
                .Where(photo => photo.ProductId == productId)
                .ToListAsync();

            return photos;
        }

        public void DeleteRange(IEnumerable<ProductImage> productImages)
            => dbContext.RemoveRange(productImages);
    }
}
