using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserReviewRepository : IGenericRepository<Review>
    {
        Task<PagedList<Review>> GetUserReviewsAsync(int userId, ReviewParams reviewParams, bool trackChanges);
    }
}
