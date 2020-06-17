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

        Gorf hero;

        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            hero = new Gorf(100,100,24,36);
        }
        private void GameScreen_Load(object sender, EventArgs e)
        {

        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            if (leftArrowDown)
            {
                hero.Move("left");
            }

            if (rightArrowDown)
            {
                hero.Move("right");
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.sizeX, hero.sizeY);

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
            }
        }
    }
}



