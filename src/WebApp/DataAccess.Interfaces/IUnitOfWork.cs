using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }
        IProductImageRepository ProductImages { get; }
        IProductRepository Products { get; }
        IRoleRepository Roles { get; }
        IUserRatingRepository UserRatings { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }

        Task SaveAsync();
    }
}
