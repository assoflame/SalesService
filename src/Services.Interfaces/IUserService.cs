using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParams);
        Task<UserDto> GetUserByIdAsync(int id);
        Task RateUserAsync(int userWhoRateId, int userId, RatingCreationDto rateDto);
        Task<(IEnumerable<RatingDto> ratingsDto, MetaData metaData)> GetUserRatingsAsync(int userId, RatingParameters ratingParams);
    }
}
