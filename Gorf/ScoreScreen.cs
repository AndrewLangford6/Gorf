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
    public partial class ScoreScreen : UserControl
    {
        public ScoreScreen()
        {
            InitializeComponent();
        }

        private void ScoreScreen_Load(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            int low = 0;
            int high = scores.Count - 1;
            try
            {
                scores = scores.OrderBy(x => x.score).ToList();
                int reference = Convert.ToInt32(nameInput.Text);
                string found1 = LinearSearch(scores, reference);
                string found2 = BinarySearchRecursive(scores, low, high, reference);
                scoreInput.Text = found2;
                nameRemove.Text = found1;
            }
            catch
            {
                outputLabel.Text = "must put in int";
            }
        }
    }
}
