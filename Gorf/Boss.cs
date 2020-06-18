using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorf
{
    class Boss
    {

        Random randGenBoss = new Random();
        int rBoss;

        public int x, y, sizeX, sizeY, hp;

        public Boss(int _x, int _y, int _sizeX, int _sizeY, int _hp)
        {
            x = _x;
            y = _y;
            sizeX = _sizeX;
            sizeY = _sizeY;
            hp = _hp;

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

        public void lower()
        {
            y = y + 2;
        }

        public void Glide(string direction)
        {
            if (direction == "left")
            {
                x = x - 3;
            }

            if (direction == "right")
            {
                x = x + 3;
            }
        }
    }
}
