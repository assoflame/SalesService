using SalesService.Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        void CreateRange(IEnumerable<ProductImage> productImages);
        void DeleteRange(IEnumerable<ProductImage> productImage);
        Task<IEnumerable<ProductImage>> GetProductPhotosAsync(int productId);
    }
}
