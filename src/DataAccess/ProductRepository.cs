using DataAccess.Extensions;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<Product>> GetAllProductsAsync(
            ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .Include(product => product.Images)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges: false)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .CountAsync();

            return new PagedList<Product>
                (products, count, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product?> GetProductByIdAsync(int id, bool trackChanges)
            => await FindByCondition(product => product.Id == id, trackChanges)
                .Include(product => product.Images)
                .FirstOrDefaultAsync();

        public async Task<PagedList<Product>> GetUserProductsAsync(
            int userId,
            ProductParameters productParameters,
            bool trackChanges)
        {
            var userProducts = await FindByCondition(product => product.UserId == userId, trackChanges)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .Include(product => product.Images)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(product => product.UserId == userId, trackChanges: false)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .CountAsync();

            return new PagedList<Product>(userProducts, count, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product?> GetUserProductAsync(int userId, int productId, bool trackChanges)
            => await FindByCondition(product => product.UserId == userId && product.Id == productId, trackChanges)
                .Include(product => product.Images)
                .FirstOrDefaultAsync();
    }
}
