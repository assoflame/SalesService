using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductDto(int Id, int UserId, string Name, string Description,
        decimal Price, bool IsSold, DateTime CreationDate);
}