﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public record ProductDto(Guid Id, string Name, Guid CategoryId);

}
