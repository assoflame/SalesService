namespace DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }
        IProductImageRepository ProductImages { get; }
        IProductRepository Products { get; }
        IRoleRepository Roles { get; }
        IUserReviewRepository Reviews { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }

        Task SaveAsync();
    }
}
