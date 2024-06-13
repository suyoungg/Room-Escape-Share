using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_Escape_by_VISIONARIES
{
    public partial class frmM2Stove : Form
    {
        //GLOBAL Variables
        Label label;
        Label firstChoice = null;
        Label secondChoice = null;
        Random rnd = new Random();
        List<string> icons = new List<string>() //list with icons
    {
        "👕", "👕", "👮🏼‍‍", "👮🏼‍‍", "*", "*", "🏛", "🏛",
        "!", "!", "💣", "💣", "🏴", "🏴", "?", "?"
    };
        public frmM2Stove()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmM2Stove_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            Icons(); //randomly assign icons
        }
        public void Icons()
        {
            for (int i = 0; i < tableGrid.Controls.Count; i++)
            {
                if (tableGrid.Controls[i] is Label)
                {
                    label = (Label)tableGrid.Controls[i];
                }
                else
                {
                    continue;
                }
                int randomNum = rnd.Next(0, icons.Count);
                label.Text = icons[randomNum];
                icons.RemoveAt(randomNum);
            }
        }

        private void lbls_Click(object sender, EventArgs e)
        {
            //if timer is on, return value so user can't click more than one icon
            if (timer.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            //When labels are clicked
            if (clickedLabel != null) //if the icon becomes visible.
            {
                if (clickedLabel.ForeColor == Color.White) //if icon is visible
                    return;

                if (firstChoice == null) //if first click, clickedLabel is white
                {
                    firstChoice = clickedLabel;
                    clickedLabel.ForeColor = Color.White;
                    return;
                }

                //Any second click
                secondChoice = clickedLabel;
                secondChoice.ForeColor = Color.White;
                endGame();
                //if the same images are clicked
                if (firstChoice.Text == secondChoice.Text)
                {
                    firstChoice = null;
                    secondChoice = null;
                    endGame();
                    return;
                }
                timer.Start();
            }
        }

        private void endGame()
        {
            //if all labels through the grid have an icon, program exits
            for (int i = 0; i < tableGrid.Controls.Count; i++)
            {
                label = tableGrid.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }
            this.Close();
        }

        //Timer function
        private void timer_Run(object sender, EventArgs e) //When timer runs out after clicking unmatched icons
        {
            timer.Stop();
            firstChoice.ForeColor = firstChoice.BackColor;
            secondChoice.ForeColor = secondChoice.BackColor;
            firstChoice = null;
            secondChoice = null;
        }

        //Help //? icon at top
        private void lblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click the boxes to uncover image. Match images.");
        }
    }

}

