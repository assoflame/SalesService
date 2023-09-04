using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRatingRepository
    {
        void Create(UserRating rating);

        Task<PagedList<UserRating>> GetUserRatings(int userId, RatingParameters ratingParams, bool trackChanges);
    }
}
