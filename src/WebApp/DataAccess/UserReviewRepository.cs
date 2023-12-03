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
    public class UserReviewRepository : GenericRepository<Review>, IUserReviewRepository
    {
        public UserReviewRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<Review>> GetUserReviewsAsync(int userId, ReviewParams reviewParams)
        {
            var userReviews = await dbContext.Reviews.Where(userReview => userReview.UserId == userId)
                .Include(userReview => userReview.UserWhoRated)
                .OrderByDescending(userReview => userReview.CreationDate)
                .Skip((reviewParams.PageNumber - 1) * reviewParams.PageSize)
                .Take(reviewParams.PageSize)
                .ToListAsync();

            var count = await dbContext.Reviews.Where(userReview => userReview.UserId == userId)
                .CountAsync();

            return new PagedList<Review>(userReviews, count, reviewParams.PageNumber, reviewParams.PageSize);
        }
    }
}
