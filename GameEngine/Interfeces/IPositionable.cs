﻿using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfeces
{
    public interface IPositionable
    {
        int X { get; }

        int Y { get; }

        Direction Direction { get; }
    }
}
