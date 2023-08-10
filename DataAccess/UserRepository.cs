using DataAccess.Interfaces;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await base.GetAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await FindByIdAsync(id);
        }

        public async Task CreateAsync(User user) => await base.CreateAsync(user);

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter = null,
                                                         Func<IQueryable<User>,
                                                                 IIncludableQueryable<User, object>>
                                                             include = null,
                                                         Func<IQueryable<User>, IOrderedQueryable<User>>
                                                             orderBy = null)
        {
            return (await base.GetAsync(filter, include, orderBy)).FirstOrDefault();
        }

        void IUserRepository.Update(User user) => base.Update(user);
    }
}
