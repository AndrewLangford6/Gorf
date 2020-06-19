using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;

namespace Gorf
{
    class Minion
    {
        
        public int x, y, sizeX, sizeY, hp2, mIFrames, sprite;
        int scale = 5;
        int timerR = 0;
        int timerL = 0;

        public Minion(int _x, int _y, int _sizeX, int _sizeY, int _hp, int _mIFrames, int _sprite)
        {
            x = _x;
            y = _y;
            sizeX = _sizeX;
            sizeY = _sizeY;
            hp2 = _hp;
            mIFrames = _mIFrames;
            sprite = _sprite;

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
            timerL++;
            timerR++;

            if (direction == "left")
            {
                x = x - 2;
                if (timerL > 10)
                {
                    sprite = 1;
                }
                else
                {
                    sprite = 2;
                }

                if (timerL > 20)
                {
                    timerL = 0;
                }
            }

            if (direction == "right")
            {
                x = x + 2;
                if (timerR > 10)
                {
                    sprite = 3;
                }
                else
                {
                    sprite = 4;
                }

                if (timerR > 20)
                {
                    timerR = 0;
                }
            }

            if (direction == "stop")
            {
                x = x;
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
