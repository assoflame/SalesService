namespace Services.Interfaces
{
    public interface IAdminService
    {
        Task BlockUser(int userId);
    }
}
