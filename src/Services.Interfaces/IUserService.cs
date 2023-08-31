using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task RateUser(int userId, int sellerId, RateDto rateDto);
    }
}
