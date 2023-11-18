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
    public class UserRatingRepository : GenericRepository<UserRating>, IUserRatingRepository
    {
        public UserRatingRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<UserRating>> GetUserRatingsAsync(int userId, RatingParameters ratingParams, bool trackChanges)
        {
            var userRatings = await FindByCondition(userRating => userRating.UserId == userId, trackChanges)
                .Include(userRating => userRating.UserWhoRated)
                .OrderByDescending(userRating => userRating.CreationDate)
                .Skip((ratingParams.PageNumber - 1) * ratingParams.PageSize)
                .Take(ratingParams.PageSize)
                .ToListAsync();

            var count = await FindByCondition(userRating => userRating.UserId == userId, trackChanges: false)
                .CountAsync();

            return new PagedList<UserRating>(userRatings, count, ratingParams.PageNumber, ratingParams.PageSize);
        }
    }
}
