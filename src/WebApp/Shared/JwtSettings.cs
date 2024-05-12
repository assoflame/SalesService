using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudience { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public int AccessExpiresInMunutes { get; set; }
        public int RefreshExpiresInDays => 7;
    }
}
