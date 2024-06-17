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

    public partial class frmM2CutBoard : Form
    {
        //Order as bools in
        bool Cheese = false;
        bool Knife = false;
        bool Potato = false;
        bool Pan = false;
        bool Oil = false;
        bool Gravy = false;

        public frmM2CutBoard()
        {
            InitializeComponent();
        }

        private void frmM2CutBoard_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void picCheese_Click(object sender, EventArgs e)
        {
            if (Oil == false && Potato == false && Knife == false && Pan == false && Gravy == false) //if first clicked
            {
                picCheese.Location = new Point(12, 353);
                Cheese = true;
            }
            else if (Cheese == true)
            {
                picCheese.Location = new Point(12, 353);
                Cheese = true;
            }
            else //if order not followed
            {
                picCheese.Location = new Point(144, 126);
                Cheese = false;
            }
        }

        private void picKnife_Click(object sender, EventArgs e)
        {
            if (Cheese == true)
            {
                picKnife.Location = new Point(144, 353);
                Knife = true;
            }
            else if (Knife == true)
            {
                picKnife.Location = new Point(144, 353);
                Knife = true;
            }
            else //mess
            {
                picCheese.Location = new Point(144, 126);
                picKnife.Location = new Point(284, 126);
                Knife = false;
                Cheese = false;
            }
        }

        private void picPotato_Click(object sender, EventArgs e)
        {
            if (Cheese == true && Knife == true)
            {
                picPotato.Location = new Point(284, 353); //only change x
                Potato = true;
            }
            else if (Potato == true)
            {
                picPotato.Location = new Point(284, 353); //only change x
                Potato = true;
            }
            else
            {

                picCheese.Location = new Point(144, 126);
                picKnife.Location = new Point(284, 126);
                picPotato.Location = new Point(559, 126);
                Cheese = false;
                Knife = false;
                Potato = false;
            }
        }

        private void picPan_Click(object sender, EventArgs e)
        {
            if (Cheese == true && Knife == true && Potato == true)
            {
                picPan.Location = new Point(427, 353); //only change x
                Pan = true;
            }
            else if (Pan == true)
            {
                picPan.Location = new Point(427, 353); //only change x
                Pan = true;
            }
            else //mess
            {
                picCheese.Location = new Point(144, 126);
                picKnife.Location = new Point(284, 126);
                picPotato.Location = new Point(559, 126);
                picPan.Location = new Point(683, 126);
                Cheese = false;
                Knife = false;
                Potato = false;
                Pan = false;
            }
        }

        private void picOil_Click(object sender, EventArgs e)
        {
            if (Cheese == true && Knife == true && Potato == true && Pan == true)
            {
                picOil.Location = new Point(559, 353); //only change x
                Oil = true;
            }
            else if (Oil == true)
            {
                picPan.Location = new Point(559, 353); //only change x
                Pan = true;
            }
            else //mess
            {
                picCheese.Location = new Point(144, 126);
                picKnife.Location = new Point(284, 126);
                picPotato.Location = new Point(559, 126);
                picPan.Location = new Point(683, 126);
                picOil.Location = new Point(12, 126);

                Cheese = false;
                Knife = false;
                Potato = false;
                Pan = false;
                Oil = false;
            }
        }

        

        private void picGravy_Click(object sender, EventArgs e)
        {
            if (Cheese == true && Knife == true && Potato == true && Pan == true && Oil == true)
            {
                this.Close();
            }
            else/* if (Cheese != true && Knife != true && Potato != true && Pan != true && Oil != true)*///mess
            {
                picCheese.Location = new Point(144, 126);
                picKnife.Location = new Point(284, 126);
                picPotato.Location = new Point(559, 126);
                picPan.Location = new Point(683, 126);
                picOil.Location = new Point(12, 126);
                picGravy.Location = new Point(427, 126);
                Cheese = false;
                Knife = false;
                Potato = false;
                Pan = false;
                Oil = false;
                Gravy = false;
            }
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click ingredients in the correct order to make poutine.");
        }
    }
}
