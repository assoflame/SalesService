using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ChatDto(UserDto FirstUser, UserDto SecondUser, DateTime CreationDate, MessageDto[] messages);
}
