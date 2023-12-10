namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IProductService ProductService { get; }
        IAuthService AuthService { get; }
        IAdminService AdminService { get; }
        IChatService ChatService { get; }
    }
}
