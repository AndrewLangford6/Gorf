using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorf
{
    public class Bullet
    {
        public int pX, pY, pH, pW;


        public int pShootingCounter = 0;

        public bool shooting = false;

        public Bullet(int x, int y, int h, int w)
        {
            pX = x;
            pY = y;
            pW = w;
            pH = h;


        }

        public void Move(string direction)
        {
            if (direction == "left")
            {
                pX = pX + 5;
            }

            if (direction == "right")
            {
                pX = pX - 5;
            }


        }
        public void Shoot(string direction)
        {
            if (direction == "left")
            {
                pX = pX - 18;
            }

            if (direction == "right")
            {
                pX = pX + 18;
            }

            if (direction == "up")
            {
                pY = pY - 18;
            }

            if (direction == "down")
            {
                pY = pY + 18;
            }
        }
    }
}
