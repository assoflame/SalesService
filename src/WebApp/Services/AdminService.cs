using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task BlockUser(int userId)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId);

            user.Status = UserStatus.Blocked;

            await _unitOfWork.Products
                .DeleteUserProducts(userId);

            await _unitOfWork.SaveAsync();
        }
    }
}
