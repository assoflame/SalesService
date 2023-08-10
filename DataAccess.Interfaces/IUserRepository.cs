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
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserByIdAsync(int id, bool trackChanges);
        Task<User> GetUserByEmailAsync(string email, bool trackChanges);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
    }
}
