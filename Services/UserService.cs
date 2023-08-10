using AutoMapper;
using DataAccess.Interfaces;
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

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetUsersAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetUserAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public Task BlockUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
