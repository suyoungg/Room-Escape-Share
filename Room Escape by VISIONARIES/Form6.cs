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
    public partial class frmMini1 : Form
    {
        //GLOBAL Variables
        Graphics g;
        Bitmap backbufferM;                  //This will hold the image to be drawn on the Form
        frmStorage frmG = new frmStorage();  //Access image of the spot the difference game

        //Spot the difference picture boxes
        PictureBox Differences;

        //Booleans for differences
        bool pantsL = true;
        bool pantsR = true;
        bool hairL = true;
        bool hairR = true;
        bool shoesL = true;
        bool shoesR = true;
        bool badgeL = true;
        bool badgeR = true;


        //Counter for number of differences found
        int count = 0;
        public frmMini1()
        {
            InitializeComponent();
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click on the parts that is different from the other!");
        }

        private void frmMini1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            this.WindowState = FormWindowState.Normal;
            backbufferM = new Bitmap(frmG.picMini1.Image, 550, 500);
            picPantsL();
            picPantsR();
            picHairL();
            picHairR();
            picShoesL();
            picShoesR();
            picBadgeL();
            picBadgeR();
        }

        private void frmMini1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(backbufferM, 76, 125, backbufferM.Width, backbufferM.Height);
        }

        public void leaveMini()
        {
            if (count >= 4)
            {
                this.Close();
            }
        }

        ////Pants DIFFERENCES
        //Pants Left
        public void picPantsL()
        {
            Differences = new PictureBox();
            Differences.BringToFront();
            Differences.Width = 67;
            Differences.Height = 200;
            Differences.Location = new Point(153, 345);
            Differences.Click += PantsL_Click;           //Interaction when Left pic pants are clicked
            Differences.BackColor = Color.Transparent;   //Make it transparent so when user click Left pic pants they are actually clicking this picbox
            Controls.Add(Differences);                   //Make the pic boxs do what we code
        }

        public void PantsL_Click(object sender, EventArgs e) //Click Event for Left pants pic
        {
            //disable pants pic boxes, check if user found all diffs
            if (pantsR == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                pantsL = false;
                pantsR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        //Pants Right
        public void picPantsR()
        {
            Differences = new PictureBox();
            Differences.BringToFront();
            Differences.Width = 67;
            Differences.Height = 200;
            Differences.Location = new Point(465, 350);
            Differences.Click += PantsR_Click;             //Interaction when Right pic pants are clicked
            Differences.BackColor = Color.Transparent;     //Make it transparent so when user click Right pic pants they are actually clicking this pic
            Controls.Add(Differences);                     //Make the pic boxs do what we code
        }

        public void PantsR_Click(object sender, EventArgs e) //Click Event for Right pants pic
        {
            //disable pants pic boxes, check if user found all diffs
            if (pantsR == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                pantsL = false;
                pantsR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        ////HAIR DIFFERENCE
        //Hair Left
        public void picHairL()
        {
            Differences = new PictureBox();
            Differences.SendToBack();
            Differences.Width = 65;
            Differences.Height = 80;
            Differences.Location = new Point(157, 170);
            Differences.Click += HairL_Click;            //Interaction when Left pic hair is clicked
            Differences.BackColor = Color.Transparent;   //Make it transparent so when user click Left pic hair they are actually clicking this picbox

            Controls.Add(Differences);
        }

        public void HairL_Click(object sender, EventArgs e)  //Click Event for Left hair pic
        {
            //disable hair pic boxes, check if user found all diffs
            if (hairL == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                hairL = false;
                hairR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        //Hair Right
        public void picHairR()
        {
            Differences = new PictureBox();
            Differences.SendToBack();
            Differences.Width = 65;
            Differences.Height = 80;
            Differences.Location = new Point(468, 170);
            Differences.Click += HairR_Click;            //Interaction when Right pic hair are clicked
            Differences.BackColor = Color.Transparent;   //Make it transparent so when user click Right pic Hair they are actually clicking this picbox


            Controls.Add(Differences);
        }

        public void HairR_Click(object sender, EventArgs e) //Click Event for Right hair pic
        {

            //disable hair pic boxes, check if user found all diffs
            if (hairR == true)
            {
                //DifferencesEyes.BringToFront();
                //Differences.SendToBack();
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                hairL = false;
                hairR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        ////SHOES DIFFERENCES
        //Shoes Left
        public void picShoesL()
        {
            Differences = new PictureBox();
            Differences.Width = 100;
            Differences.Height = 60;
            Differences.Location = new Point(130, 550);
            Differences.Click += ShoesL_Click;          //Interaction when Left pic shoes are clicked

            Differences.BackColor = Color.Transparent;                                   //Make it transparent so when user click bed they are actually clicking this picbox

            Controls.Add(Differences);
        }


        public void ShoesL_Click(object sender, EventArgs e)  //Click Event for Left shoes pic
        {
            //disable shoes pic boxes, check if user found all diffs
            if (shoesL == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                shoesL = false;
                shoesR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        //Shoes Right
        public void picShoesR()
        {
            Differences = new PictureBox();
            Differences.Width = 100;
            Differences.Height = 60;
            Differences.Location = new Point(439, 550);
            Differences.Click += ShoesR_Click;          //Interaction when Right pic shoes are clicked

            Differences.BackColor = Color.Transparent;                                   //Make it transparent so when user click bed they are actually clicking this picbox

            Controls.Add(Differences);
        }

        public void ShoesR_Click(object sender, EventArgs e) //Click Event for Right shoes pic
        {
            //disable hair pic boxes, check if user found all diffs
            if (shoesR == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                shoesL = false;
                shoesR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        ////BADGE DIFFERENCES
        //Badge Left
        public void picBadgeL()
        {
            Differences = new PictureBox();
            Differences.Width = 20;
            Differences.Height = 20;
            Differences.Location = new Point(200, 275);
            Differences.Click += BadgeL_Click;           //Interaction when Left pic bagde is clicked
            Differences.BackColor = Color.Transparent;  //Make it transparent so when user click Left pic badge they are actually clicking this picbox

            Controls.Add(Differences);
        }

        public void BadgeL_Click(object sender, EventArgs e)  //Click Event for Left badge pic
        {
            //disable badge pic boxes, check if user found all diffs
            if (badgeL == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                badgeL = false;
                badgeR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }

        //Badge Right
        public void picBadgeR()
        {
            Differences = new PictureBox();
            Differences.Width = 20;
            Differences.Height = 20;
            Differences.Location = new Point(502, 275);
            Differences.Click += BadgeR_Click;          //Interaction when Right pic badge is clicked

            Differences.BackColor = Color.Transparent;                                   //Make it transparent so when user click bed they are actually clicking this picbox

            Controls.Add(Differences);
        }

        public void BadgeR_Click(object sender, EventArgs e)  //Click Event for Right badge pic
        {
            //disable badge pic boxes, check if user found all diffs
            if (badgeR == true)
            {
                count++;
                lblCount.Text = $"Differences Found: {count}/4";
                badgeL = false;
                badgeR = false;
                leaveMini();
            }
            else
            {
                MessageBox.Show("Difference already found.");
            }
        }
    }
}
