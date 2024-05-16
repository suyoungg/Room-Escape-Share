using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_Escape_by_VISIONARIES
{
    public partial class frmMain : Form
    {
        
        Graphics g;
        frmStorage frmG = new frmStorage();          //This is where picturebox and etc are stored
        Bitmap backbuffer1;                          //For room1     
        Bitmap backbuffer2;                          //For room2   
        Bitmap sprite;
        Bitmap dialog;
        int characSize = 100;       
        Rectangle rectDest;

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Make every image screen size
            backbuffer1 = new Bitmap(frmG.picBack1.Image,Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);



           // Warden sprite location in background 2(LTBD)
            int spriteX = Screen.PrimaryScreen.Bounds.Width - characSize;
            int spriteY = Screen.PrimaryScreen.Bounds.Height - 550;
            sprite = new Bitmap(frmG.picSprite.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(backbuffer1, 0, 0, backbuffer1.Width, backbuffer1.Height);

            g.DrawImage(sprite, rectDest);
        }

        private void picArrowL_Click(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            g.DrawImage(backbuffer2, 0, 0, backbuffer2.Width, backbuffer2.Height);
        }


        private void picArrowR_Click(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            g.DrawImage(backbuffer1, 0, 0, backbuffer1.Width, backbuffer1.Height);
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
//Goals
//Find where to put dialogue box and how to make it appear
//Git hub
//Bring clickable item and make it able to touch and interact
//Item box? (Make user able to posses item)
//Make it move in to next level
//Character idle (If time allows)
