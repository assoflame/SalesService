using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParams);
        Task<UserDto> GetUserByIdAsync(int id);
        Task RateUserAsync(int userWhoRateId, int userId, ReviewCreationDto rateDto);
        Task<(IEnumerable<ReviewDto> reviewsDto, MetaData metaData)>
            GetUserReviewsAsync(int userId, ReviewParams reviewParams);
    }
}
