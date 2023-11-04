using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        private const int maxPageSize = 30;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public string? SearchString { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
