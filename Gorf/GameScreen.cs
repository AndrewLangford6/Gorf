using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Gorf
{
    public partial class GameScreen : UserControl
    {
        // create a random number generator
        Random randGen = new Random();

        
        SoundPlayer chargePlayer = new SoundPlayer(Properties.Resources.CHARGE);
        SoundPlayer startP = new SoundPlayer(Properties.Resources.iMac_Startup_Chime_Carlo__1849294605);

        SoundPlayer bossLaserPlayer = new SoundPlayer(Properties.Resources.BLAST);

        //used to draw boxes on screen
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush BrownBrush = new SolidBrush(Color.Brown);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush pinkBrush = new SolidBrush(Color.Pink);

        Gorf hero;
        Floor ground;
        Boss boss;

        List<Bullet> bR = new List<Bullet>();
        List<Bullet> bL = new List<Bullet>();
        List<Bullet> bUp = new List<Bullet>();
        List<Bullet> bDown = new List<Bullet>();

        List<Minion> mList = new List<Minion>();

        List<Egg> eList = new List<Egg>();

        public static int gravity, gravityCounter, iFrames, hp, mHP, score;
        public static int jumpSpeed;

        public static string scoreInt;

        int heroX = ((720 / 2) - 12);

        bool facingR, mIsFacingR;
        int pShootingCounter, pH, pW, hpX, hpY, hpL, bulletDamage, eggTime, rando, rando2, bossTime, bHPFInal, LaserTime, rando3, bossTime2, bossTime3, bossTime4, bossTime5, bossExT, gorfSprite, walkCounterR, walkCounterL, mWalkingCR, mWalkingCL, mSprite;
        float bHPPP;
        double bHPPPROUND;

        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, sDown, xDown;
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            startP.Play();
            rightArrowDown = leftArrowDown = sDown = xDown = downArrowDown = upArrowDown = false;
            winLoseLabel.Visible = false;
            hero = new Gorf(heroX, 100, 24, 36);

            ground = new Floor(0, 380, 720, 70);

            boss = new Boss(Width / 2 - 155, -400, 310, 90, 250);

            hp = 34;
            gorfSprite = 5;
            score = 0;

            jumpSpeed = 0;
            gravity = -18;
            gravityCounter = 0;

            pShootingCounter = 0;
            pH = 4;
            pW = 48;
            bulletDamage = 3;

            facingR = true;

            rando2 = 150;
        }
        public void Win()
        {
            gameLoop.Stop();
            scoreInt = Convert.ToString(score);
            winLoseLabel.Visible = true;
            winLoseLabel.Text = "YOU WIN!\n Score: " + score;
            
            Thread.Sleep(3000);
            gameLoop.Stop();

            

            Form1.ChangeScreen(this, "ScoreScreen");
        }
        public void GameOver()
        {
            
            scoreInt = Convert.ToString(score);
            winLoseLabel.Visible = true;
            winLoseLabel.Text = "Game Over!\n Score: " + score;
            
            Thread.Sleep(3000);

            gameLoop.Stop();

            Form1.ChangeScreen(this, "ScoreScreen");


        }
        private void GameScreen_Load(object sender, EventArgs e)
        {

        }

        public void gameLoop_Tick(object sender, EventArgs e)
        {
            score++;

            if (boss.hp <= 0)
            {
                Win();
            }
                Rectangle gorf = new Rectangle(hero.x, hero.y, hero.sizeX, hero.sizeY);
            Rectangle bossRect = new Rectangle(boss.x, boss.y - 60, boss.sizeX, boss.sizeY);
            //boss
            bossTime++;
            bossTime3++;
            bossExT++;
            LaserTime++;
            bHPPP = boss.hp * (496)/250 * 2;
            bHPPPROUND = Math.Round(bHPPP);
            bHPFInal = Convert.ToInt32(bHPPPROUND);
            if(boss.y + boss.sizeY <= 179)
            {
                boss.lower();
            }
            else
            {

            }

            if(boss.x + boss.sizeX >= ground.x + ground.sizeX + 60)
            {
                bossTime2 = 0;
                bossTime = 0;
                bossTime2++;


                if (bossTime2 >= 275)
                {
                    bossTime2 = 0;
                    bossTime = 0;
                }
                else
                {
                    boss.Glide("left");

                }
            }
            if(boss.x <= ground.x - 60)
            {
                bossTime2 = 0;
                bossTime = 0;
                bossTime2++;


                if (bossTime2 >= 275)
                {
                    bossTime2 = 0;
                    bossTime = 0;
                }
                else
                {
                        boss.Glide("right");
                    
                }
            }

            if (bossTime3 >= 200)
            {
                Rando3();
                bossTime3 = 0;
            }

            if(bossTime >= 300)
            {
                bossTime2++;
                

                if (bossTime2 >= 150)
                {
                    bossTime2 = 0;
                    bossTime = 0;
                }
                else
                {
                    
                    if (rando3 == 1)
                    {
                        boss.Glide("left");
                    }
                    else
                    {
                        boss.Glide("right");
                    }
                }

                
            }

            if(LaserTime >= 450)
            {
                chargePlayer.Play();
            }
            if (LaserTime >= 600)
            {
                bossLaserPlayer.Play();
                bossTime4++;
                

                if (bossTime4 >= 200)
                {
                    
                    bossTime4 = 0;
                    LaserTime = 0;
                    bossLaserPlayer.Stop();
                }
                else
                {
                    Rectangle laser = new Rectangle(boss.x + 124, boss.y + boss.sizeY - 20, 62, 700);

                    if (laser.IntersectsWith(gorf))
                    {
                        bossTime5++;

                        if(bossTime5 >= 10)
                        {
                            hero.Hurt();
                            bossTime5 = 0;
                        }
                        
                    }
                }
            }

            //hero movement
            if(hp <= 0 || hero.y + hero.sizeY>= Height)
            {
                GameOver();
               
            }

            gravityCounter++;
            iFrames++;
            eggTime++;
            foreach (Minion m in mList)
            {
                m.mIFrames++;
            }
            hpL = hp;
            hpX = hero.x - 6;
            hpY = hero.y - 12;



            if (gravityCounter >= 2)
            {
                gravityCounter = 0;
            }
            hero.Passive();

             
            

            walkCounterR++;
            if (leftArrowDown)
            {
                Moving("right");

                if (walkCounterR > 3)
                {
                    gorfSprite = 3;
                }
                else
                {
                    gorfSprite = 4;
                }

                if (walkCounterR > 6)
                {
                    walkCounterR = 0;
                }
            }

            walkCounterL++;
            if (rightArrowDown)
            {
                Moving("left");
                if (walkCounterL > 3)
                {
                    gorfSprite = 1;
                }
                else 
                {
                    gorfSprite = 2;
                }

                if (walkCounterL > 6)
                {
                    walkCounterL = 0;
                }
            }

            

            if (sDown)
            {
                hero.Jump();
                
            }

            if (upArrowDown)
            {
                gorfSprite = 6;
            }

            if (downArrowDown)
            {
                foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
                {
                    bullet.Move("down");
                }
                gorfSprite = 5;
                if (!downArrowDown)
                {
                    if (facingR)
                    {
                        gorfSprite = 1;
                    }
                    else
                    {
                        gorfSprite = 3;
                    }
                }

            }

            

            //minions
            foreach (Minion m in mList)
            {
                Rectangle minionRect = new Rectangle(m.x, m.y, m.sizeX, m.sizeY);

                if (m.x + m.sizeX < hero.x + 1)
                {
                    m.Chase("right");
                    mIsFacingR = true;
                }
                else if (m.x > hero.x + hero.sizeX - 1)
                {
                    m.Chase("left");
                    mIsFacingR = false;
                }
                else
                {
                    mIsFacingR = mIsFacingR;
                }
            }
            //collisions

            //hero
            if (hero.x + hero.sizeX >= ground.x && hero.x <= ground.x + ground.sizeX)
            {
                if (hero.y >= 310 + hero.sizeY)
                {
                    hero.y = 310 + hero.sizeY;
                    sDown = false;
                    gravity = -18;
                    jumpSpeed = 0;
                }
            }

            //minion


            foreach (Minion m in mList)
            {
                Rectangle minionRect = new Rectangle(m.x, m.y, m.sizeX, m.sizeY);

                if (minionRect.IntersectsWith(minionRect))
                {
                    m.Chase("stop");
                }

                if (minionRect.IntersectsWith(gorf) && iFrames >= 20)
                {
                    hero.Hurt();
                    if (mIsFacingR)
                    {
                        Moving("left");
                    }
                    else
                    {

                        Moving("right");
                    }

                    iFrames = 0;
                }


            }

            //minion and ground
            foreach (Minion m in mList.AsEnumerable().Reverse())
            {

                m.Passive();

                if (m.y > 485)
                {
                    mList.Remove(m);

                }

                if (m.x + m.sizeX >= ground.x && m.x <= ground.x + ground.sizeX)
                {
                    if (m.y >= 310 + m.sizeY)
                    {
                        m.y = 310 + m.sizeY;
                    }
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
                Rectangle b1 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);

                foreach (Minion m in mList.AsEnumerable().Reverse())
                {
                    Rectangle minionRect = new Rectangle(m.x, m.y, m.sizeX, m.sizeY);

                    if (b1.IntersectsWith(minionRect) && m.mIFrames > 20)
                    {
                        m.hp2 = m.hp2 - bulletDamage;
                        m.Hurt("right");
                        m.mIFrames = 0;

                        bR.Remove(bullet);
                    }
                    if (m.hp2 <= 0)
                    {
                        mList.Remove(m);
                    }
                }
            }
            //left bullets
            foreach (Bullet bullet in bL.AsEnumerable().Reverse())
            {
                bullet.Shoot("left");
                Rectangle b2 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);

                foreach (Minion m in mList.AsEnumerable().Reverse())
                {
                    Rectangle minionRect = new Rectangle(m.x, m.y, m.sizeX, m.sizeY);

                    if (b2.IntersectsWith(minionRect) && m.mIFrames > 20)
                    {
                        m.hp2 = m.hp2 - bulletDamage;
                        m.Hurt("left");
                        m.mIFrames = 0;
                        bL.Remove(bullet);
                    }
                    if (m.hp2 <= 0)
                    {
                        mList.Remove(m);

                    }
                }
            }
            //up bullets
            foreach (Bullet bullet in bUp.AsEnumerable().Reverse())
            {
                bullet.Shoot("up");
                Rectangle b3 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);

                if (b3.IntersectsWith(bossRect))
                {
                    boss.hp = boss.hp - bulletDamage;
                    bUp.Remove(bullet);
                }

            }
            //down bullets
            foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
            {
                bullet.Shoot("down");
                Rectangle b4 = new Rectangle(bullet.pX, bullet.pY, bullet.pW, bullet.pH);
                foreach (Minion m in mList.AsEnumerable().Reverse())
                {
                    Rectangle minionRect = new Rectangle(m.x, m.y, m.sizeX, m.sizeY);

                    if (b4.IntersectsWith(minionRect) && m.mIFrames > 20)
                    {
                        m.hp2 = m.hp2 - bulletDamage;
                        m.Hurt("left");
                        m.mIFrames = 0;
                        bDown.Remove(bullet);
                    }
                    if (m.hp2 <= 0)
                    {
                        mList.Remove(m);

                    }
                }
            }

            if (xDown)
            {
                if (pShootingCounter > 20)
                {
                    
                    //right
                    if (facingR == true)
                    {
                        Bullet b1 = new Bullet(hero.x + hero.sizeX + 6, hero.y + 8, pH, pW);
                        //up
                        if (upArrowDown)
                        {
                            Bullet b3 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2) - 9, hero.y - hero.sizeY - 12, pW, pH);
                            b3.Sound();
                            bUp.Add(b3);
                        }
                        //down
                        else if (downArrowDown)
                        {
                            Bullet b4 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2) - 9, hero.y + hero.sizeY - 6, pW, pH);
                            b4.Sound();
                            bDown.Add(b4);
                        }
                        else
                        {
                            b1.Sound();
                            bR.Add(b1);
                        }

                    }
                    //left
                    if (facingR == false)
                    {
                        Bullet b2 = new Bullet(hero.x - 6 - pW, hero.y + 8, pH, pW);
                        //up
                        if (upArrowDown)
                        {
                            Bullet b3 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2) - 9, hero.y - hero.sizeY - 12, pW, pH);
                            b3.Sound();
                            bUp.Add(b3);
                        }
                        //down
                        else if (downArrowDown)
                        {
                            Bullet b4 = new Bullet(hero.x + (hero.sizeX / 2) - (pH / 2) - 9, hero.y + hero.sizeY - 6, pW, pH);
                            b4.Sound();
                            bDown.Add(b4);
                        }
                        else
                        {
                            b2.Sound();
                            bL.Add(b2);
                        }

                    }

                    pShootingCounter = 0;
                }


            }

            //egg
            foreach (Egg p in eList.AsEnumerable().Reverse())
            {
                p.Passive();


                if (p.x + p.sizeX >= ground.x && p.x <= ground.x + ground.sizeX)
                {
                    if (p.y + p.sizeY >= 310 + p.sizeY)
                    {
                        p.t++;
                        p.y = 310;
                        p.sprite = 2;

                        if (p.t > 25)
                        {
                            
                            Minion eggMinion = new Minion(p.x + 12, p.y + 24, 24, 36, 12, 0, 1);
                            mList.Add(eggMinion);

                            eList.Remove(p);
                        }

                    }
                }
            }

            if (eggTime > rando2)
            {
                rando2 = randGen.Next(100, 400);
                rando = randGen.Next(ground.x, ground.x + ground.sizeX - 48);
                Egg test2 = new Egg(rando, -100, 48, 72, 0, 1);
                eList.Add(test2);

                eggTime = 0;
            }

            Refresh();
        }

        public void Rando3()
        {
            rando3 = randGen.Next(1, 3);
        }

        public void Sprites(object sender, PaintEventArgs e)
        {
            
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            Rectangle gorf = new Rectangle(hero.x - 18, hero.y - 18, hero.sizeX + 18, hero.sizeY + 18);
            

            Rectangle hpP = new Rectangle(hpX -8, hpY - 17, hpL, 6);
            Rectangle hpB = new Rectangle(hpX-9, hpY - 18, 36, 8);

            Rectangle bHPB = new Rectangle(112, 420, 498, 18);
            Rectangle bHPP = new Rectangle(113, 421, bHPFInal/2, 17);



            Rectangle bossRect = new Rectangle(boss.x, boss.y - 60, boss.sizeX, boss.sizeY + 60);

            
            //e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.sizeX, hero.sizeY);
            

            e.Graphics.FillRectangle(whiteBrush, hpB);
            e.Graphics.FillRectangle(greenBrush, hpP);

            

            //SPRITES
            if (gorfSprite == 1)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Walk_R1, gorf);
            }
            else if (gorfSprite == 2)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Walk_R3, gorf);
            }
            else if (gorfSprite == 3)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Walk_L1, gorf);
            }
            else if (gorfSprite == 4)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Walk_L3, gorf);
            }
            else if (gorfSprite == 5)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Down, gorf);
            }
            else if (gorfSprite == 6)
            {
                e.Graphics.DrawImage(Properties.Resources.Gorf_Up, gorf);
            }


            

            //


            foreach (Minion m in mList)
            {
                Rectangle minionRect = new Rectangle(m.x - 12, m.y -20, m.sizeX+24, m.sizeY+20);

                //SPRITES
                if (m.sprite == 1)
                {
                    e.Graphics.DrawImage(Properties.Resources.Alien1, minionRect);
                }
                else if (m.sprite == 2)
                {
                    e.Graphics.DrawImage(Properties.Resources.Alien2, minionRect);
                }
                else if (m.sprite == 3)
                {
                    e.Graphics.DrawImage(Properties.Resources.AlienR1, minionRect);
                }
                else if (m.sprite == 4)
                {
                    e.Graphics.DrawImage(Properties.Resources.AlienR2, minionRect);
                }

            }


            //right bullets
            foreach (Bullet bullet in bR.AsEnumerable().Reverse())
            {


                Rectangle b1 = new Rectangle(bullet.pX, bullet.pY-2, bullet.pW, bullet.pH - 3);
                e.Graphics.FillRectangle(pinkBrush, b1);

                if (bullet.pX > this.Width + 100)
                {
                    bR.Remove(bullet);
                }
            }
            //left bullets
            foreach (Bullet bullet in bL.AsEnumerable().Reverse())
            {


                Rectangle b2 = new Rectangle(bullet.pX, bullet.pY-2, bullet.pW, bullet.pH -3);
                e.Graphics.FillRectangle(pinkBrush, b2);

                if (bullet.pX < -100)
                {
                    bR.Remove(bullet);
                }
            }
            //up bullet
            foreach (Bullet bullet in bUp.AsEnumerable().Reverse())
            {


                Rectangle b3 = new Rectangle(bullet.pX+1, bullet.pY, bullet.pW - 3, bullet.pH);
                e.Graphics.FillRectangle(pinkBrush, b3);

                if (bullet.pY < 0)
                {
                    bUp.Remove(bullet);
                }
            }
            //down bullet
            foreach (Bullet bullet in bDown.AsEnumerable().Reverse())
            {


                Rectangle b4 = new Rectangle(bullet.pX + 1, bullet.pY, bullet.pW - 3, bullet.pH);
                e.Graphics.FillRectangle(pinkBrush, b4);

                if (bullet.pY > 500)
                {
                    bDown.Remove(bullet);
                }
            }


            foreach (Egg eg in eList.AsEnumerable().Reverse())
            {
                Rectangle eggo = new Rectangle(eg.x - 20, eg.y - 20, eg.sizeX + 40, eg.sizeY + 50);
                if (eg.sprite == 1)
                {
                    e.Graphics.DrawImage(Properties.Resources.Egg, eggo);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.Egg_dead, eggo);
                }
            }



            Rectangle laser = new Rectangle(boss.x + 124, boss.y + boss.sizeY - 20, 62, 700);
           
            if(LaserTime >= 600)
            {
                e.Graphics.FillRectangle(pinkBrush, laser);
            }
            
            if(boss.hp > 0)
            {
                e.Graphics.DrawImage(Properties.Resources.boss, bossRect);
            }
            else
            {
                if (bossExT > 5)
                {
                    e.Graphics.DrawImage(Properties.Resources.boss_ded1, bossRect);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.boss_ded2, bossRect);
                }

                if (bossExT > 10)
                {
                    bossExT = 0;
                }
                
                
            }

            e.Graphics.FillRectangle(BrownBrush, ground.x, ground.y, ground.sizeX, ground.sizeY);

            e.Graphics.FillRectangle(whiteBrush, bHPB);
            e.Graphics.FillRectangle(greenBrush, bHPP);
        }

        private void Moving(string direction)
        {
            if (direction == "right")
            {
                ground.Move("left");

                boss.Move("left");

                //minions
                foreach (Minion m in mList)
                {
                    m.Move("left");
                }

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

                foreach (Egg e in eList)
                {
                    e.Move("left");
                }
            }

            if (direction == "left")
            {
                ground.Move("right");

                boss.Move("right");

                //minions
                foreach (Minion m in mList)
                {
                    m.Move("right");
                }

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

                foreach (Egg e in eList)
                {
                    e.Move("right");
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



