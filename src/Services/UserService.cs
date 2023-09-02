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

        public async Task RateUser(int customerId, int sellerId, RateDto rateDto)
        {
            var seller = await _unitOfWork.Users.GetUserByIdAsync(sellerId, trackChanges: false);

            if (seller is null)
                throw new UserNotFoundException(sellerId);

            var rating = new UserRating
            {
                CustomerId = customerId,
                SellerId = sellerId,
                StarsCount = rateDto.StarsCount,
                Comment = rateDto.Comment
            };

            _unitOfWork.UserRatings.Create(rating);

            await _unitOfWork.SaveAsync();
        }
    }
}
