using AutoMapper;
using DataAccess.Interfaces;
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

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, mapper));

        }

        public IUserService UserService => _userService.Value;
    }
}
