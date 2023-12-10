using SalesService.Entities.Models;
using Shared.RequestFeatures;

namespace DataAccess.Interfaces
{
    public interface IUserReviewRepository : IGenericRepository<Review>
    {
        Task<PagedList<Review>> GetUserReviewsAsync(int userId, ReviewParams reviewParams);
    }
}
