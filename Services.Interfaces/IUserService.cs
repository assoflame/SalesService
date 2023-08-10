using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges);
        Task<UserDto> GetUserByIdAsync(int id, bool trackChanges);
    }
}
