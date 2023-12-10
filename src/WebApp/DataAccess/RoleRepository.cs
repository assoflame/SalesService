using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;

namespace DataAccess
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context) : base(context) { }

        public async Task<Role?> GetByNameAsync(string name)
            => await dbContext.Roles.Where(role => role.Name == name)
                .FirstOrDefaultAsync();
    }
}
