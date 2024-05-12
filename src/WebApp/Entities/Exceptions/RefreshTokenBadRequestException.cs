using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RefreshTokenBadRequestException : Exception
    {
        public RefreshTokenBadRequestException() : base("Unable to refresh token")
        {
        }
    }
}
