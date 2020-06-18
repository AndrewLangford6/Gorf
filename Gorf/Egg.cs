using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorf
{
    class Egg
    {
        public int x, y, sizeX, sizeY, t;

        public Egg(int _x, int _y, int _sizeX, int _sizeY, int _t)
        {
            x = _x;
            y = _y;
            sizeX = _sizeX;
            sizeY = _sizeY;
            t = _t;

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
        public void Passive()
        {
            y = y + 2;
        }
    }

}
