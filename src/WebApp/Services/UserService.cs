using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(IEnumerable<UserDto> users, MetaData metaData)>
            GetAllUsersAsync(UserParameters userParams)
        {
            var usersWithMetaData = await _unitOfWork
                .Users
                .GetAllUsersAsync(userParams, trackChanges: false);

            var users = _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);

            return (users: users, metaData: usersWithMetaData.MetaData);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id, trackChanges: false);

            if (user is null)
                throw new UserNotFoundException(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<(IEnumerable<ReviewDto> reviewsDto, MetaData metaData)>
            GetUserReviewsAsync(int userId, ReviewParams reviewParams)
        {
            var reviews = await _unitOfWork
                .Reviews
                .GetUserReviewsAsync(userId, reviewParams, trackChanges: false);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            return (reviewsDto: reviewsDto, metaData: reviews.MetaData);
        }

        public async Task RateUserAsync(int userWhoRateId, int userId, ReviewCreationDto rateDto)
        {
            var seller = await _unitOfWork.Users.GetUserByIdAsync(userId, trackChanges: false);

            if (seller is null)
                throw new UserNotFoundException(userId);

            var review = new Review
            {
                UserWhoRatedId = userWhoRateId,
                UserId = userId,
                StarsCount = rateDto.StarsCount,
                Comment = rateDto.Comment,
                CreationDate = DateTime.UtcNow
            };

            _unitOfWork.Reviews.Create(review);

            await _unitOfWork.SaveAsync();
        }
    }
}
