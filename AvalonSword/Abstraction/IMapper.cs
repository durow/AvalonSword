﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword
{
    public interface IMapper
    {
        T Map<T>(object src);
    }
}
