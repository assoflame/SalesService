using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserAsync(int id);
        Task BlockUserAsync(int id);
        Task CreateUserAsync();
        //Task UpdateUserAsync(int id, UserForUpdateDto userForUpdate);
    }
}
