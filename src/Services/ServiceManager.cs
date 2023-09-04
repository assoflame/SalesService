using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IAdminService> _adminService;
        private readonly Lazy<IChatService> _chatService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger,
            IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(
                () => new UserService(unitOfWork, mapper, logger));

            _productService = new Lazy<IProductService>(
                () => new ProductService(unitOfWork, mapper, logger));

            _authService = new Lazy<IAuthService>(
                () => new AuthService(unitOfWork, mapper, logger, configuration));

            _adminService = new Lazy<IAdminService>(
                () => new AdminService(unitOfWork, mapper, logger));

            _chatService = new Lazy<IChatService>(
                () => new ChatService(unitOfWork, mapper, logger));
        }

        public IUserService UserService => _userService.Value;
        public IProductService ProductService => _productService.Value;
        public IAuthService AuthService => _authService.Value;
        public IAdminService AdminService => _adminService.Value;
        public IChatService ChatService => _chatService.Value;
    }
}
