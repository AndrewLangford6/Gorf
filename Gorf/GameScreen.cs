using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gorf
{
    public partial class GameScreen : UserControl
    {
        //used to draw boxes on screen
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush BrownBrush = new SolidBrush(Color.Brown);

        Gorf hero;
        Floor ground;

        List<Bullet> bR = new List<Bullet>();
        List<Bullet> bL = new List<Bullet>();
        List<Bullet> bUp = new List<Bullet>();
        List<Bullet> bDown = new List<Bullet>();

        public static int gravity, gravityCounter;
        public static int jumpSpeed;

        bool facingR;
        int pShootingCounter, pH, pW;

        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, sDown, xDown;
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            hero = new Gorf(((this.Width / 2) - 12), 100, 24, 36);

            ground = new Floor(0, 380, 720, 70);

            jumpSpeed = 0;
            gravity = -18;
            gravityCounter = 0;

            pShootingCounter = 0;
            pH = 4;
            pW = 36;

            facingR = true;


        }
        private void GameScreen_Load(object sender, EventArgs e)
        {

        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //hero movement
            gravityCounter++;
            if (gravityCounter >= 2)
            {
                gravityCounter = 0;
            }
            hero.Passive();

            if (leftArrowDown)
            {
                ground.Move("left");

                //right bullets
                foreach (Bullet bullet in bR.AsEnumerable().Reverse())
                {
                    bullet.Move("left");
                }
                //left bullets
                foreach (Bullet bullet in bL.AsEnumerable().Reverse())
                {
                    bullet.Move("left");
                }
            }

            if (rightArrowDown)
            {
                ground.Move("right");

                //right bullets
                foreach (Bullet bullet in bR.AsEnumerable().Reverse())
                {
                    bullet.Move("right");
                }
                //left bullets
                foreach (Bullet bullet in bL.AsEnumerable().Reverse())
                {
                    bullet.Move("right");
                }
            }

            if (sDown)
            {
                hero.Jump();
            }

            if (upArrowDown)
            {

            }

            if (downArrowDown)
            {
                foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
                {
                    bullet.Move("down");
                }
            }

            //collisions

            //hero
            if (hero.x + hero.sizeX >= ground.x && hero.x <= ground.x + ground.sizeX)
            {
                if (hero.y > 310 + hero.sizeY)
                {
                    hero.y = 310 + hero.sizeY;
                    sDown = false;
                    gravity = -18;
                    jumpSpeed = 0;
                }
            }

            //bullet 

            //bullet hits ground
            foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
            {
                Rectangle bGround = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                Rectangle groundB = new Rectangle(ground.x, ground.y, ground.sizeX, ground.sizeY);

                if (bGround.IntersectsWith(groundB))
                {
                    bDown.Remove(bullet);
                }
            }

            //Bullets
            pShootingCounter++;

            //right bullets
            foreach (Bullet bullet in bR.AsEnumerable().Reverse())
            {
                bullet.Shoot("right");
            }
            //left bullets
            foreach (Bullet bullet in bL.AsEnumerable().Reverse())
            {
                bullet.Shoot("left");
            }
            //up bullets
            foreach (Bullet bullet in bUp.AsEnumerable().Reverse())
            {
                bullet.Shoot("up");
            }
            //down bullets
            foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
            {
                bullet.Shoot("down");
            }

            if (xDown)
            {
                if (pShootingCounter > 20)
                {
                    //right
                    if (facingR == true)
                    {
                        Bullet b1 = new Bullet(hero.x + hero.sizeX + 6, hero.y + 10, pH, pW);
                        //up
                        if (upArrowDown)
                        {
                            Bullet b3 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2), hero.y - hero.sizeY - 6, pW, pH);
                            bUp.Add(b3);
                        }
                        //down
                        else if (downArrowDown)
                        {
                            Bullet b4 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2), hero.y + hero.sizeY + 6, pW, pH);
                            bDown.Add(b4);
                        }
                        else
                        {
                            bR.Add(b1);
                        }

                    }
                    //left
                    if (facingR == false)
                    {
                        Bullet b2 = new Bullet(hero.x - 6 - pW, hero.y + 10, pH, pW);
                        //up
                        if (upArrowDown)
                        {
                            Bullet b3 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2), hero.y - hero.sizeY - 6, pW, pH);
                            bUp.Add(b3);
                        }
                        //down
                        else if (downArrowDown)
                        {
                            Bullet b4 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2), hero.y + hero.sizeY + 6, pW, pH);
                            bDown.Add(b4);
                        }
                        else
                        {
                            bL.Add(b2);
                        }

                    }

                    pShootingCounter = 0;
                }


            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(BrownBrush, ground.x, ground.y, ground.sizeX, ground.sizeY);
            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.sizeX, hero.sizeY);


            //right bullets
            foreach (Bullet bullet in bR.AsEnumerable().Reverse())
            {


                Rectangle b1 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                e.Graphics.FillRectangle(whiteBrush, b1);

                if (bullet.pX > this.Width + 100)
                {
                    bR.Remove(bullet);
                }
            }
            //left bullets
            foreach (Bullet bullet in bL.AsEnumerable().Reverse())
            {


                Rectangle b2 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                e.Graphics.FillRectangle(whiteBrush, b2);

                if (bullet.pX < -100)
                {
                    bR.Remove(bullet);
                }
            }
            //up bullet
            foreach (Bullet bullet in bUp.AsEnumerable().Reverse())
            {


                Rectangle b3 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                e.Graphics.FillRectangle(whiteBrush, b3);

                if (bullet.pY < 0)
                {
                    bUp.Remove(bullet);
                }
            }
            //down bullet
            foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
            {


                Rectangle b4 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                e.Graphics.FillRectangle(whiteBrush, b4);

                if (bullet.pY > 500)
                {
                    bDown.Remove(bullet);
                }
            }
        }


        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    facingR = false;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    facingR = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.X:
                    xDown = true;
                    break;
                case Keys.Space:
                    sDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.X:
                    xDown = false;
                    break;
                    //case Keys.Space:
                    //    sDown = false;
                    //    break;
            }
        }
    }
}



