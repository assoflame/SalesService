﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductForUpdateDto(int Id, string Name, string Description, string City, decimal Price);
}
