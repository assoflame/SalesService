using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task BlockUser(int userId)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId, trackChanges: true);

            if (user == null)
                throw new UserNotFoundException(userId);

            user.Status = UserStatus.Blocked;

            await _unitOfWork.SaveAsync();
        }
    }
}
