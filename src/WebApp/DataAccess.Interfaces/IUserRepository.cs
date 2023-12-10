using SalesService.Entities.Models;
using Shared.RequestFeatures;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParams);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
