using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FactoryClients
    {
        public HttpClient UnauthorizedClient { get; set; }
        public HttpClient AuthorizedClient { get; set; }
        public HttpClient AdminClient { get; set; }
    }
}
