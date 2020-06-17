﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gorf
{
    class Gorf
    {
        public SolidBrush gorfBrush;
        public int x, y, sizeX, sizeY;

        public Gorf(int _x, int _y, int _sizeX, int _sizeY)
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
                x = x - 5;
            }

            if (direction == "right")
            {
                x = x + 5;
            }

            if (direction == "up")
            {
                y = y - 8;
            }

            if (direction == "down")
            {
                y = y + 5;
            }

        }
        public void Passive()
        {
            y = y + GameScreen.jumpSpeed;
            GameScreen.jumpSpeed++;
            if (GameScreen.jumpSpeed > 30)
            {
                GameScreen.jumpSpeed = 30;
            }
        }

        public void Jump()
        {
            if(GameScreen.gravityCounter == 2)
            {
                GameScreen.gravity++;
            }
            

            if (GameScreen.gravity > 0)
            {
                GameScreen.jumpSpeed++;

            }

            y = y + GameScreen.gravity;
        }



    }
}
