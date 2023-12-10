using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using Shared.RequestFeatures;

namespace DataAccess
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParams)
        {
            var products = await dbContext.Users
                .OrderBy(user => string.Concat(user.FirstName, user.LastName))
                .Skip((userParams.PageNumber - 1) * userParams.PageSize)
                .Take(userParams.PageSize)
                .ToListAsync();

            var count = await dbContext.Users
                .CountAsync();

            return new PagedList<User>(products, count, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User?> GetUserByIdAsync(int id)
            => await dbContext.Users.Where(user => user.Id == id)
                        .Include(user => user.Roles)
                            .ThenInclude(ur => ur.Role)
                        .Include(user => user.ReviewsAsSeller)
                            .ThenInclude(ur => ur.UserWhoRated)
                    .FirstOrDefaultAsync();
                    

        public async Task<User?> GetUserByEmailAsync(string email)
            => await dbContext.Users.Where(user => user.Email == email)
                .Include(user => user.Roles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync();
    }
}
