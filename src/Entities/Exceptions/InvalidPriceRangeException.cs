using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class InvalidPriceRangeException : Exception
    {
        public InvalidPriceRangeException() : base("Максимальная цена не может быть меньше минимальной.")
        {

        }
    }
}
