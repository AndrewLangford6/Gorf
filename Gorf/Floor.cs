﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorf
{
    class Floor
    {

        public int x, y, sizeX, sizeY;

        public Floor(int _x, int _y, int _sizeX, int _sizeY)
        {
            x = _x;
            y = _y;
            sizeX = _sizeX;
            sizeY = _sizeY;

        }
        public void Move(string direction)
        {
            if (direction == "left")
            {
                x = x + 5;
            }

            if (direction == "right")
            {
                x = x - 5;
            }

        }
    }
}
