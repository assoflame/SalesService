using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> FilterByPrice(this IQueryable<Product> query, decimal minPrice, decimal maxPrice)
            => query
                .Where(product => product.Price >= minPrice && product.Price <= maxPrice);
        
        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return query;

            return query
                .Where(product => product.Name.ToLower().Contains(searchString.ToLower()));
        }
    }
}
