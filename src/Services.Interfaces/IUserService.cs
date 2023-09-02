using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParams);
        Task<UserDto> GetUserByIdAsync(int id);
        Task RateUser(int userId, int sellerId, RateDto rateDto);
    }
}
