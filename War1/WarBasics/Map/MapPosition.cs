﻿using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics
{
    public class MapPosition
    {
        public MapPosition() { }
        public MapPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

    }
}
