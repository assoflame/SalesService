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

        public async Task<(IEnumerable<RatingDto> ratingsDto, MetaData metaData)>
            GetUserRatingsAsync(int userId, RatingParameters ratingParams)
        {
            var ratings = await _unitOfWork
                .UserRatings
                .GetUserRatingsAsync(userId, ratingParams, trackChanges: false);

            var ratingsDto = _mapper.Map<IEnumerable<RatingDto>>(ratings);

            return (ratingsDto: ratingsDto, metaData: ratings.MetaData);
        }

        public async Task RateUserAsync(int userWhoRateId, int userId, RatingCreationDto rateDto)
        {
            var seller = await _unitOfWork.Users.GetUserByIdAsync(userId, trackChanges: false);

            if (seller is null)
                throw new UserNotFoundException(userId);

            var rating = new UserRating
            {
                UserWhoRatedId = userWhoRateId,
                UserId = userId,
                StarsCount = rateDto.StarsCount,
                Comment = rateDto.Comment
            };

            _unitOfWork.UserRatings.Create(rating);

            await _unitOfWork.SaveAsync();
        }
    }
}
