using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Chats = new ChatRepository(_context);
        }

        public IChatRepository Chats { get; }
        public IMessageRepository Messages { get; }
        public IProductImageRepository ProductImages { get; }
        public IProductRepository Products { get; }
        public IRoleRepository Roles { get; }
        public IUserRatingRepository UserRatings { get; }
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
