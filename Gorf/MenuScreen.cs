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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }
        


        private void button_Enter(object sender, EventArgs e)
        {
            //start by changing all the buttons to the default colour
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                    c.BackColor = Color.White;
            }

            //change the current button to the active colour
            Button btn = (Button)sender;
            btn.BackColor = Color.Gold;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
             Form1.ChangeScreen(this, "GameScreen");
        }

        private void scoreScreenButton_Click_1(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, "ScoreScreen");
        }

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
