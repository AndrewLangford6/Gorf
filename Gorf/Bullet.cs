using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Gorf
{
    public class Bullet
    {
        public int pX, pY, pH, pW;

        SoundPlayer gorfLaserPLayer = new SoundPlayer(Properties.Resources.Laser_SoundBible_com_602495617);

        public int pShootingCounter = 0;
        public int bSpeed = 12;

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
                pX = pX - bSpeed;
            }

            if (direction == "right")
            {
                pX = pX + bSpeed;
            }

            if (direction == "up")
            {
                pY = pY - bSpeed;
            }

            if (direction == "down")
            {
                pY = pY + bSpeed;
            }
        }

        public void Sound()
        {
            gorfLaserPLayer.Play();
        }
    }
}
