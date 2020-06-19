using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Gorf
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();
            

            // open the main menu for the game
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        public static void ChangeScreen(UserControl current, string next)
        {
            //f is set to the form that the current control is on
            Form f = current.FindForm();
            f.Controls.Remove(current);
            UserControl ns = null;

            ///If any screens, (UserControls), are added to the program they need to
            ///be added within this switch block as well.
            switch (next)
            {
                case "MenuScreen":
                    ns = new MenuScreen();
                    break;
                case "GameScreen":
                    ns = new GameScreen();
                    break;
                case "ScoreScreen":
                    ns = new ScoreScreen();
                    break;
            }

            f.Controls.Add(ns);
            ns.Focus();
        }
    }
}
