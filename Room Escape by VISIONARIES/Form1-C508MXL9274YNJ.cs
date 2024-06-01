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
        Bitmap sprite3;
        int characSize = 600;
        Rectangle rectDest;
        Rectangle rectSource, rectD, rect0;
        int dialogueWidth = 900;                    //Dialogue size and declaration
        int dialogueHeight = 250;
        Label DialogueBox;

        //Cliackable item as picbox
        public PictureBox Beds;
        PictureBox faucets;
        PictureBox LightBulb;
        PictureBox Towel;
        PictureBox Cup;
        PictureBox ExitPortal;
        PictureBox CellDoor;
            
        int faucet = 0;

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Make every background image full screen size
            backbuffer1 = new Bitmap(frmG.picBack1.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //Dialog Box
            //Whenever Dialogue needed, call Dialogue Procedure and write apropriate text. Must call the click event at the end to make sure it is removed after!
            Dialogue();
            DialogueBox.Text = "You were framed for a crime and are sentenced to life in prison.\nYou don't think you deserve this. It's time to escape.";
            DialogueBox.Click += DialogueBox_Click;                                 //Create Click Event handler

            //Disable Background
            backgroundLock();

            //Arrow location
            int arrowX = Screen.PrimaryScreen.Bounds.Width - 110;
            int arrowY = Screen.PrimaryScreen.Bounds.Height - 150;
            int arrowXL = Screen.PrimaryScreen.Bounds.Width - 1380; //1380 for school comp
            picArrowR.Location = new Point(arrowX, arrowY);
            picArrowL.Location = new Point(arrowXL, arrowY);
           
            // Warden sprite location in background 2
            int spriteX = Screen.PrimaryScreen.Bounds.Width - characSize;
            int spriteY = Screen.PrimaryScreen.Bounds.Height - 650;
            sprite = new Bitmap(frmG.picSprite.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);


            //Riddler
            sprite3 = new Bitmap(frmG.picRiddler.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);
        }

        private void sprite_click(object sender, EventArgs e)
        {

        }


        private void Dialogue()
        {
            //Create a new Label for Dialogue Box
            DialogueBox = new Label();
            DialogueBox.Width = dialogueWidth;
            DialogueBox.Height = dialogueHeight;
            DialogueBox.Location = new Point(280, 580);
           // DialogueBox.Location = new Point(280, 400);
            DialogueBox.BackColor = Color.Black;            
            DialogueBox.ForeColor = Color.White;
            DialogueBox.Font = new Font("Courier New", 16, FontStyle.Regular);
            DialogueBox.BorderStyle = BorderStyle.FixedSingle;
            DialogueBox.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(DialogueBox);                                             //This create the label to the controls.
        }
        private void DialogueBox_Click(object sender, EventArgs e)
        {
            DialogueBox.Hide();
            DialogueBox.Paint += new PaintEventHandler(Dialogue_paint);
            picArrowL.Enabled = true;
            picArrowR.Enabled = true;                                             //User must click dialogue or else they won't be able to access any controls.
        }
        private void Dialogue_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image repairDestroyedback = backbuffer2;                                       
            rectSource = new Rectangle(280, 580, Beds.Width, Beds.Height);
            rectD = new Rectangle(0, 0, Beds.Width, Beds.Height);
            g.DrawImage(repairDestroyedback, rectD, rectSource, GraphicsUnit.Pixel);       
        }

        private void backgroundLock()                                             //Disable controls
        {
            picArrowL.Enabled = false;
            picArrowR.Enabled = false;
        }

        //CLICKABLE ITEMS
        public void picBeds()
        {
            Beds = new PictureBox();
            Beds.Width = 560;
            Beds.Height = 390;
            Beds.Location = new Point(65, 390);
            Beds.Click += beds_click;
            Beds.Paint += new PaintEventHandler(beds_paint);                      //For repairing destroyed area
            Controls.Add(Beds);                     
        }
        
        /*If we make Beds color transparent, parts of it destroy backbuffer 2 and displays backbuffer1. Instead of making picBeds transparent,
        beds_paint even will make picbeds display image of backbuffer2 where picbeds is located*/
        public void beds_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer2;                                        //Since picBeds destroy background, imageblend repair the destroyed background image 
            rectSource = new Rectangle(65, 390, Beds.Width, Beds.Height);
            rectD = new Rectangle(0, 0, Beds.Width, Beds.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);        //Where picbed is located
        }

        public void beds_click(object sender, EventArgs e)                        //Click Event. When user click picpeds dialogue box appear
        {
            Dialogue();
            DialogueBox.BringToFront();                                            //Make sure dialogue box goes on top of every layer
            DialogueBox.Text = "Found a piece of faucet from the beds.";
            backgroundLock();                                                      //Call procedure that locks all controls until user make dialogue disappear
            DialogueBox.Click += DialogueBox_Click;
            faucet++;
        }

        private void picfaucets()
        {
            faucets = new PictureBox();
            faucets.Width = 200;
            faucets.Height = 220;
            faucets.Location = new Point(1120, 470);
            faucets.Click += faucets_click;
            faucets.Paint += new PaintEventHandler(faucets_paint);
            Controls.Add(faucets);
        }
        private void faucets_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer2;
            rectSource = new Rectangle(1120, 470, faucets.Width, faucets.Height);
            rectD = new Rectangle(0, 0, faucets.Width, faucets.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }

        private void faucets_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "The faucet is broken and missing pieces.";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }


        private void picLightBulb()
        {
            LightBulb = new PictureBox();
            LightBulb.Width = 140;
            LightBulb.Height = 100;
            LightBulb.Location = new Point(650, 20);
            LightBulb.Click += LightBulb_click;
            LightBulb.Paint += new PaintEventHandler(LightBulb_paint);
            Controls.Add(LightBulb);
        }
        private void LightBulb_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer2;
            rectSource = new Rectangle(650, 20, LightBulb.Width, LightBulb.Height);
            rectD = new Rectangle(0, 0, LightBulb.Width, LightBulb.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }

        private void LightBulb_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "Light Bulb looks rusty and old.";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }

        private void picTowel()
        {
            Towel = new PictureBox();
            Towel.Width = 70;
            Towel.Height = 100;
            Towel.Location = new Point(770, 570);
            Towel.Click += towel_click;
            Towel.Paint += new PaintEventHandler(towel_paint);
            Controls.Add(Towel);
        }
        private void towel_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer2;
            rectSource = new Rectangle(770, 570, Towel.Width, Towel.Height);
            rectD = new Rectangle(0, 0, Towel.Width, Towel.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }

        private void towel_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "You found a piece of faucet from the towel.";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }

        private void picCup()
        {
            Cup = new PictureBox();
            Cup.Width = 70;
            Cup.Height = 80;
            Cup.Location = new Point(770, 470);
            Cup.Click += cup_click;
            Cup.Paint += new PaintEventHandler(cup_paint);
            Controls.Add(Cup);
        }

        private void cup_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer2;
            rectSource = new Rectangle(770, 470, Cup.Width, Cup.Height);
            rectD = new Rectangle(0, 0, Cup.Width, Cup.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }

        private void cup_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "You obtained a cup!";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }

        private void picExitPortal()
        {
            ExitPortal = new PictureBox();
            ExitPortal.Width = 60;
            ExitPortal.Height = 70;
            ExitPortal.Location = new Point(350, 295);
            ExitPortal.Paint += new PaintEventHandler(exitportal_paint);
            ExitPortal.Click += ExitPortal_click;
            Controls.Add(ExitPortal);
        }
        private void exitportal_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer1;
            rectSource = new Rectangle(350, 295, ExitPortal.Width, ExitPortal.Height);
            rectD = new Rectangle(0, 0, ExitPortal.Width, ExitPortal.Height);

            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }
        private void ExitPortal_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "Need Key to insert.";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }

        private void picCellDoor()
        {
            CellDoor = new PictureBox();
            CellDoor.Width = 470;
            CellDoor.Height = 740;
            CellDoor.Location = new Point(520, 50);
            CellDoor.Paint += new PaintEventHandler(cellDoor_paint);
            CellDoor.Click += CellDoor_click;
            Controls.Add(CellDoor);
        }
        private void cellDoor_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image ImageBlend = backbuffer1;
            rectSource = new Rectangle(520, 50, CellDoor.Width, CellDoor.Height);
            rectD = new Rectangle(0, 0, CellDoor.Width, CellDoor.Height);
            g.DrawImage(ImageBlend, rectD, rectSource, GraphicsUnit.Pixel);
        }
        private void CellDoor_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.BringToFront();
            DialogueBox.Text = "The cell door is locked.";
            backgroundLock();
            DialogueBox.Click += DialogueBox_Click;
        }





        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(backbuffer1, 0, 0, backbuffer1.Width, backbuffer1.Height);
            g.DrawImage(sprite, rectDest);
            picCellDoor();
            picExitPortal();

            //Never forget this.invalidate and gback.dispose
        }

        private void PrisonBackground1()
        {
            g.DrawImage(backbuffer1, 0, 0, backbuffer1.Width, backbuffer1.Height);
            g.DrawImage(sprite, rectDest);

            Controls.Remove(faucets);
            Controls.Remove(Cup);
            Controls.Remove(Towel);
            Controls.Remove(LightBulb);
            Controls.Remove(Beds);
            //Removing method INEFFECTIVE AS FUCK SOLVE IT

        }

        private void PrisonBackground2()
        {
            g.DrawImage(backbuffer2, 0, 0, backbuffer2.Width, backbuffer2.Height);
            picBeds();
            picfaucets();
            picLightBulb();
            picTowel();
            picCup();
        }

        private void picArrowL_Click(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            PrisonBackground2();
            Controls.Remove(ExitPortal);
        }

        private void picArrowR_Click(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            PrisonBackground1();
        }
    }
}
//Goals
//Maybe make dialogue box diappear by click event AND arrow click...
//Dont just put sprite, label from arrow click, but put them as a set with background..
//FIX background lock so it works on any background
//Item box? (Make user able to posses item)
//Make it move in to next level
//Character idle (If time allows)

