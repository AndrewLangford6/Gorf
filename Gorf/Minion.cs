using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorf
{
    class Minion
    {
        public int x, y, sizeX, sizeY, hp2, mIFrames;
        int scale = 5;

        public Minion(int _x, int _y, int _sizeX, int _sizeY, int _hp, int _mIFrames)
        {
            x = _x;
            y = _y;
            sizeX = _sizeX;
            sizeY = _sizeY;
            hp2 = _hp;
            mIFrames = _mIFrames;

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

        public void Chase(string direction)
        {
            if (direction == "left")
            {
                x = x - 2;
            }

            if (direction == "right")
            {
                x = x + 2;
            }
        }
        public void Hurt(string direction)
        {
            if (direction == "left")
            {

                scale++;
                x = x - scale;

                if (scale > 10)
                {
                    scale = 5;
                }
            }

            if (direction == "right")
            {
                scale++;
                x = x + scale;

                if (scale > 10)
                {
                    scale = 5;
                }

            }
        }
        public void Passive()
        {
            y = y + 8;
        }
    }

}
