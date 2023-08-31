using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class TooManyImagesCountException : Exception
    {
        public TooManyImagesCountException(int maxImagesCount) :
            base($"You cannot add more than {maxImagesCount} images to one product.")
        {

        }
    }
}
