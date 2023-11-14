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
        
        public static IQueryable<Product> Search(this IQueryable<Product> query, string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return query;

            return query
                .Where(product => product.Name.ToLower().Contains(searchString.ToLower()));
        }

        public static IQueryable<Product> Sort(this IQueryable<Product> products,
            string? orderByQueryParam)
        {
            if (string.IsNullOrEmpty(orderByQueryParam))
                return products.OrderBy(product => product.Price);

            orderByQueryParam = orderByQueryParam.ToLower();

            switch(orderByQueryParam)
            {
                case "name" : return products
                        .OrderBy(product => product.Name);
                case "price": return products.
                        OrderBy(product => product.Price);
                case "date": return products
                        .OrderBy(product => product.CreationDate);
                default: return products
                        .OrderByDescending(product => product.Price);
            }
        }
    }
}
