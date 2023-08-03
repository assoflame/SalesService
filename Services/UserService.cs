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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetUsers();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public Task<UserDto> GetUserAsync(int id)
        {
            return null;
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
