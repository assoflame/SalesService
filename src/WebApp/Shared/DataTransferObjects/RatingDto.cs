using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RatingDto(int UserWhoRatedId, int UserId, int starsCount, string? Comment);
}
