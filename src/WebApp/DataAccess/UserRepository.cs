using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
