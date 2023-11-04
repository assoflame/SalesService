using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRatingRepository : IGenericRepository<UserRating>
    {
        Task<PagedList<UserRating>> GetUserRatingsAsync(int userId, RatingParameters ratingParams, bool trackChanges);
    }
}
