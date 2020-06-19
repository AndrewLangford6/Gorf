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
using System.Media;

namespace Gorf
{
    public partial class ScoreScreen : UserControl
    {

        List<HighScore> highList = new List<HighScore>();
        SoundPlayer music = new SoundPlayer(Properties.Resources.StardustMan);

        public ScoreScreen()
        {
            InitializeComponent();

            string newHigh, newNum;

            music.Play();

            XmlReader reader = XmlReader.Create("NewFolder/XMLFileHighScores.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {


                    reader.ReadToNextSibling("name");
                    newHigh = reader.ReadString();

                    reader.ReadToNextSibling("num");
                    newNum = reader.ReadString();



                    HighScore newH = new HighScore(newHigh, newNum);
                    highList.Add(newH);
                }
            }
            
            foreach (HighScore c in highList)
            {
                label1.Text += c.name + " "
                    + c.score + "\n";
            }

            reader.Close();
        }

        private void ScoreScreen_Load(object sender, EventArgs e)
        {
            
        }

        private void add_Click(object sender, EventArgs e)
        {
            string name, number;

            name = textBox1.Text;
            number = GameScreen.scoreInt;

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

            XmlWriter writer = XmlWriter.Create("NewFolder1/XMLFilePain.xml", null);

            writer.WriteStartElement("HighScores");

            foreach (HighScore c in highList)
            {
                writer.WriteStartElement("SCORE");
                writer.WriteElementString("name", c.name);
                writer.WriteElementString("num", c.score);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

            Form1.ChangeScreen(this, "MenuScreen");
        }
    }
}
