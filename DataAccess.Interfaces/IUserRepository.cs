using Microsoft.EntityFrameworkCore.Query;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task CreateAsync(User user);

        Task<User> GetAsync(Expression<Func<User, bool>>? filter = null,
                                                         Func<IQueryable<User>,
                                                                 IIncludableQueryable<User, object>>?
                                                             include = null,
                                                         Func<IQueryable<User>, IOrderedQueryable<User>>?
                                                             orderBy = null);
        void Update(User user);
    }
}
