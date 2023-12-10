using SalesService.Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
    }
}
