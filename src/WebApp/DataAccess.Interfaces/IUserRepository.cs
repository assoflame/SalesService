using Microsoft.EntityFrameworkCore.Query;
using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParams, bool trackChanges);
        Task<User?> GetUserByIdAsync(int id, bool trackChanges);
        Task<User?> GetUserByEmailAsync(string email, bool trackChanges);
    }
}
