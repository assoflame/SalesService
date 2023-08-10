using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using Services.Interfaces;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges)
        {
            var users = await _unitOfWork.Users.GetAllUsersAsync(trackChanges);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id, bool trackChanges)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id, trackChanges);

            if (user is null)
                throw new UserNotFoundException(id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
