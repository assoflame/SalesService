using DataAccess.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork));
        }

        public IUserService UserService => _userService.Value;
    }
}
