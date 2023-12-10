using DataAccess.Interfaces;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly Lazy<IChatRepository> _chatRepo;
        private readonly Lazy<IMessageRepository> _messageRepo;
        private readonly Lazy<IProductImageRepository> _productImageRepo;
        private readonly Lazy<IProductRepository> _productRepo;
        private readonly Lazy<IRoleRepository> _roleRepo;
        private readonly Lazy<IUserReviewRepository> _reviewRepo;
        private readonly Lazy<IUserRepository> _userRepo;
        private readonly Lazy<IUserRoleRepository> _userRoleRepo;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _chatRepo = new Lazy<IChatRepository>(() => new ChatRepository(_context));
            _messageRepo = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
            _productImageRepo = new Lazy<IProductImageRepository>(() => new ProductImageRepository(_context));
            _productRepo = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _roleRepo = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
            _reviewRepo = new Lazy<IUserReviewRepository>(() => new UserReviewRepository(_context));
            _userRepo = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _userRoleRepo = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(_context));
        }

        public IChatRepository Chats => _chatRepo.Value;
        public IMessageRepository Messages => _messageRepo.Value;
        public IProductImageRepository ProductImages => _productImageRepo.Value;
        public IProductRepository Products => _productRepo.Value;
        public IRoleRepository Roles => _roleRepo.Value;
        public IUserReviewRepository Reviews => _reviewRepo.Value;
        public IUserRepository Users => _userRepo.Value;
        public IUserRoleRepository UserRoles => _userRoleRepo.Value;

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();


        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
