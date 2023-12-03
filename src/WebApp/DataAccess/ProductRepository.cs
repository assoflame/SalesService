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
            ProductParameters productParameters)
        {
            var products = await dbContext.Products
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters?.SearchString)
                .Sort(productParameters.OrderBy!)
                .Include(product => product.Images)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

            var count = await dbContext.Products
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .CountAsync();

            return new PagedList<Product>
                (products, count, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
            => await dbContext.Products.Where(product => product.Id == id)
                .Include(product => product.Images)
                .FirstOrDefaultAsync();

        public async Task<PagedList<Product>> GetUserProductsAsync(
            int userId,
            ProductParameters productParameters)
        {
            var userProducts = await dbContext.Products.Where(product => product.UserId == userId)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .Include(product => product.Images)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

            var count = await dbContext.Products.Where(product => product.UserId == userId)
                .FilterByPrice(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchString)
                .CountAsync();

            return new PagedList<Product>(userProducts, count, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task DeleteUserProducts(int userId)
        {
            dbContext.RemoveRange(
                await dbContext.Products.Where(product => product.UserId == userId)
                .ToListAsync());

            await dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetUserProductAsync(int userId, int productId)
            => await dbContext.Products.Where(product => product.UserId == userId && product.Id == productId)
                .Include(product => product.Images)
                .FirstOrDefaultAsync();
    }
}
