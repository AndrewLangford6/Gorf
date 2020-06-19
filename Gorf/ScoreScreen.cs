using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Gorf
{
    public partial class ScoreScreen : UserControl
    {

        List<HighScore> highList = new List<HighScore>();

        public ScoreScreen()
        {
            InitializeComponent();

            string newHigh, newNum;

            XmlReader reader = XmlReader.Create("NewFolder/XMLFileHighScores.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {


                    reader.ReadToNextSibling("name");
                    newHigh = reader.ReadString();

                    reader.ReadToNextSibling("num");
                    newNum = reader.ReadString();



                    HighScore newH = new HighScore(newHigh, Convert.ToInt32(newNum));
                    highList.Add(newH);
                }
            }

            reader.Close();
        }

        private void ScoreScreen_Load(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            string name;
            int number;

            name = textBox1.Text;
            number = Convert.ToInt32(GameScreen.score);

            HighScore nH = new HighScore(name, number);

            highList.Add(nH);

            label1.Text = "";

            //for (int i = 0; i < carsList.Count; i++)
            //{
            //    outputLabel.Text += carsList[i].year + " " + carsList[i].make + " " + carsList[i].colour + " " + carsList[i].mileage + "\n";
            //}

            foreach (HighScore c in highList)
            {
                label1.Text += c.name + " "
                    + c.score + "\n";
            }
        }
    }
}
