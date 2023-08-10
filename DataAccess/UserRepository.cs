using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SalesService.Entities.Models;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(user => user.FirstName)
                .ToListAsync();

        public async Task<User> GetUserByIdAsync(int id, bool trackChanges)
            => await FindByCondition(user => user.Id == id, trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<User> GetUserByEmailAsync(string email, bool trackChanges)
            => await FindByCondition(user => user.Email == email, trackChanges)
                .Include(user => user.Roles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync();
    }
}
