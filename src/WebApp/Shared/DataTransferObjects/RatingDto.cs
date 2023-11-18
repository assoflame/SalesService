using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RatingDto(UserDto User, int StarsCount, string? Comment, DateTime CreationDate);
}
