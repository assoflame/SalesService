using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        void CreateRange(IEnumerable<ProductImage> productImages);
        void DeleteRange(IEnumerable<ProductImage> productImage);
        Task<IEnumerable<ProductImage>> GetProductPhotosAsync(int productId, bool trackChanges);
    }
}
