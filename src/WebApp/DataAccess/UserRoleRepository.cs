using DataAccess.Interfaces;
using SalesService.Entities.Models;

namespace DataAccess
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationContext context) : base(context) { }

    }
}
