using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System.Security.Claims;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UsersController(IServiceManager services)
        {
            _services = services;
        }


    }
}
