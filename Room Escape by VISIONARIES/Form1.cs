using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Room_Escape_by_VISIONARIES
{
    public partial class frmMain : Form
    {

        Graphics g;
        frmStorage frmG = new frmStorage();          //This is the form where picturebox and etc are stored
        Bitmap backbuffer1;                          //For room1     
        Bitmap backbuffer2;                          //For room2   

        //Level 2 Backgrounds
        Bitmap backbuffer3;
        Bitmap backbuffer33;
        Bitmap backbuffer4;
        Bitmap backbuffer5;
        Bitmap backbuffer6;
        //Level 3 Backgrounds
        Bitmap backbuffer7;
        Bitmap backbuffer8;
        Bitmap backbuffer9;
        Bitmap backbuffer10;

        Bitmap currentBackground;                    //When painting different backgrounds and make specific items to appear on a specific backbuffer
       
       // NPCs
        Bitmap sprite;                               //Warden
        Bitmap sprite2;                              //Emma
        Bitmap sprite3;                              //Riddler
        int characSize = 600;
        Rectangle rectDest;
        Rectangle rectSource, rectD, rect0;
        PictureBox clickSprite;

        //Dialogue Size and property
        int dialogueWidth = 900;                
        int dialogueHeight = 220;
        Label DialogueBox;

        //Cliackable item as picbox
        public PictureBox Beds;
        public PictureBox faucets;
        PictureBox LightBulb;
        public PictureBox Towel;
        PictureBox Cup;
        PictureBox ExitPortal;
        PictureBox CellDoor;
        public PictureBox Cabinet;
        //Level2
        PictureBox ElectricLock;


        //Item Box
        ListBox ItemBox;
        int itemWidth = 450;
        int itemHeight = 100;
        int faucetPiece = 0;

        //BOOLS
        bool cup;
        bool levelOneKey = false;
        bool cupFilledWater = false;
        bool cellDoorUnlock = false;
        bool hideSprite = false;                         //Bool for hiding/showing sprite NPC

        //Counters
        int countBed = 0;                 
        int countCabinet = 0;
        int countTowel = 0;


        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Make every background image full screen size
            backbuffer1 = new Bitmap(frmG.picBack1.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer3 = new Bitmap(frmG.picBack3.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            currentBackground = backbuffer1;                                          //Default background is backbuffer1. Later to be useful in Paint event for changing background

            //Dialog Box
            //Whenever Dialogue needed, call Dialogue Procedure and write apropriate text. Must call the click event to make sure it is removed after!
            Dialogue();
            DialogueBox.Text = "You were framed for a crime and are sentenced to life in prison.\nYou don't think you deserve this. It's time to escape.";
            DialogueBox.Click += DialogueBox_Click;                                   //This calls Click Event handler

            //Item Box
            Items();
            ItemBox.Items.Add("ITEM BOX");

            //Arrow location
            int arrowX = Screen.PrimaryScreen.Bounds.Width - 110;
            int arrowY = Screen.PrimaryScreen.Bounds.Height - 150;
            int arrowXL = Screen.PrimaryScreen.Bounds.Width - 1380; //1380 for school comp
            picArrowR.Location = new Point(arrowX, arrowY);                                       //Location for arrow Right
            picArrowL.Location = new Point(arrowXL, arrowY);                                      //Location for arrow Left

            // Warden sprite location in background 2
            int spriteX = Screen.PrimaryScreen.Bounds.Width - characSize;
            int spriteY = Screen.PrimaryScreen.Bounds.Height - 650;
            sprite = new Bitmap(frmG.picSprite.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);

            //Emma NPC


            //Riddler NPC
            sprite3 = new Bitmap(frmG.picRiddler.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);
        }

        private void Dialogue()
        {
            //Create a new Label for Dialogue Box
            DialogueBox = new Label();
            DialogueBox.Width = dialogueWidth;
            DialogueBox.Height = dialogueHeight;
             DialogueBox.Location = new Point(280, 600);
            //DialogueBox.Location = new Point(280, 400);
            DialogueBox.BackColor = Color.FromArgb(200, Color.Black);              //fromArgb refers to transparency
            DialogueBox.ForeColor = Color.White;
            DialogueBox.Font = new Font("Courier New", 14, FontStyle.Regular);
            DialogueBox.BorderStyle = BorderStyle.FixedSingle;
            DialogueBox.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(DialogueBox);                                             //This adds the label to the controls.
            DialogueBox.BringToFront();                                            //Bring diologue on top of every layers so picbox don't go over dialogue   
        }

        private void DialogueBox_Click(object sender, EventArgs e)                 //When user click dialogue box,
        {
            Controls.Remove(DialogueBox);                                          //Hide
        }
        //Since Bitmap don't have click event, put a transparent picbox on top of bitmap so user can click it for interaction
        private void spriteClick()
        {
            clickSprite = new PictureBox();
            clickSprite.Width = 150;
            clickSprite.Height = 500;
            clickSprite.Location = new Point(1050, 300);
            clickSprite.BackColor = Color.Transparent;               //This allows picbox to be transparent
            clickSprite.Click += clickSprite_Click;                  //Can interact with NPC through a click event
            Controls.Add(clickSprite);
        }
        private void clickSprite_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                            //Remove any previous dialogue before opening new dialogue
            if (cupFilledWater == false)                             //If user don't posses an item called cup filled with water
            {
                Dialogue();          
                DialogueBox.Text = "What are you looking at? Go back to your cell. \n God, this place is so dirty, I want to get out of here right now!";
                DialogueBox.Click += DialogueBox_Click;                                  
            }
            else if (cupFilledWater == true)                         //If user posses cup filled with water
            {
                Dialogue();
                DialogueBox.Text = "What have you done! You spilled water on me.\nNow I have to go change my clothes.\n*Leaves room and drops key*";
                DialogueBox.Click += DialogueBox_Click;
                levelOneKey = true;                                  //Posses Key to escape level 1
                ItemBox.Items.Remove("Cup filled with water");       //Remove item after it is USED
                ItemBox.Items.Add("Master Key");                     //Obtain different item 
                hideSprite = true;                                   //Refer frmPaint event. program paints sprite ONLY hidesprite == false. When hidesprite == true it won't draw the sprite.
                Invalidate();                                        //Always repaint after paint event!

            }
        }

        //User Item Box
        private void Items()
        {
            ItemBox = new ListBox();
            ItemBox.Width = itemWidth;
            ItemBox.Height = itemHeight;
            ItemBox.Location = new Point(100, 50);
            ItemBox.BackColor = Color.Black;
            ItemBox.ForeColor = Color.White;
            ItemBox.Font = new Font("Courier New", 12, FontStyle.Regular);
            ItemBox.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(ItemBox);
        }

        //CLICKABLE ITEMS
        public void picBeds()
        {
            Beds = new PictureBox();
            Beds.Width = 560;
            Beds.Height = 390;
            Beds.Location = new Point(65, 390);
            Beds.Click += beds_click;                                             //Interaction when Bed is clicked
            Beds.BackColor = Color.Transparent;                                   //Make it transparent so when user click bed they are actually clicking this picbox
            Controls.Add(Beds);
        }

        public void beds_click(object sender, EventArgs e)                        //Click Event. When user click picpeds dialogue box appear
        {
            Controls.Remove(DialogueBox);

            if (countBed == 0)
            {
                Dialogue();
                DialogueBox.Text = "Found a piece of faucet from the beds.";
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");                //Remove ANY previous faucet line so we don't have unecessary repeatings but instead merge texts together
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                DialogueBox.Click += DialogueBox_Click;
                countBed++;
            }
            else if (countBed > 0)                                                       //When it's second time clicking picBeds (After they obtained faucets)
            {
                Dialogue();
                DialogueBox.Text = "No time to sleep. It's time to escape.";
                DialogueBox.Click += DialogueBox_Click;
            }
        }

        public void picCabinet()                                                    //Repeat same procedure as picBeds
        {
            Cabinet = new PictureBox();
            Cabinet.Width = 172;
            Cabinet.Height = 170;
            Cabinet.Location = new Point(773, 280);
            Cabinet.BackColor = Color.Transparent;
            Cabinet.Click += Cabinet_Click;
            Controls.Add(Cabinet);
        }

        public void Cabinet_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);

            if (countCabinet == 0)
            {
                Controls.Remove(DialogueBox);
                Dialogue();
                DialogueBox.Text = "You found a piece of faucet from the cabinet.";
                DialogueBox.Click += DialogueBox_Click;
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                countCabinet++;
            }
            else if (countCabinet > 0)
            {
                Dialogue();
                DialogueBox.Text = "Cabinet is empty.";
                DialogueBox.Click += DialogueBox_Click;
            }
        }

        private void picfaucets()
        {
            faucets = new PictureBox();
            faucets.Width = 200;
            faucets.Height = 220;
            faucets.Location = new Point(1120, 470);
            faucets.Click += faucets_click;
            faucets.BackColor = Color.Transparent;
            Controls.Add(faucets);
        }

        private void faucets_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (faucetPiece < 3)                                 //If user hasn't gathered all faucet pieces
            {
                Dialogue();
                DialogueBox.Text = "The faucet is broken. \n You might need to find missing pieces to fix.";
                DialogueBox.Click += DialogueBox_Click;
            }
            else if (faucetPiece == 3 && cup == false)          //If user has faucets pieces but NO cups to fill water
            {
                Dialogue();
                DialogueBox.Text = "Faucet fixed. You hear water drops...";
                DialogueBox.Click += DialogueBox_Click;
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
            }
            
            //When user gathered all 3 faucet pieces AND a cup, faucet will give cup filled water
            if(faucetPiece == 3 && cup == true && cupFilledWater == false) //cupFilledWater == false is to prevent user from obtaining another cupFilledWater
            {
                Dialogue();
                DialogueBox.Text = "Cup is now filled with water.\n ...Maybe you could spill this on someone.";
                DialogueBox.Click += DialogueBox_Click;

                cupFilledWater = true;                                     //Gain item
                ItemBox.Items.Remove("Cup");
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                ItemBox.Items.Add("Cup filled with water");
            }       
        }

        private void picLightBulb()
        {
            LightBulb = new PictureBox();
            LightBulb.Width = 140;
            LightBulb.Height = 100;
            LightBulb.Location = new Point(650, 20);
            LightBulb.Click += LightBulb_click;
            LightBulb.BackColor = Color.Transparent;
            Controls.Add(LightBulb);
        }     
        private void LightBulb_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "Light Bulb looks rusty and old.";
            DialogueBox.Click += DialogueBox_Click;
        }

        private void picTowel()
        {
            Towel = new PictureBox();
            Towel.Width = 70;
            Towel.Height = 100;
            Towel.Location = new Point(770, 570);
            Towel.Click += towel_click;
            Towel.BackColor = Color.Transparent;
            Controls.Add(Towel);
        }

        private void towel_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (countTowel == 0)
            {
                Dialogue();
                DialogueBox.Text = "You found a piece of faucet from the towel.";
                DialogueBox.Click += DialogueBox_Click;
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                countTowel++;
            }
            
             else if (countTowel > 0)
            {
                Dialogue();
                DialogueBox.Text = "Dry towel.";
                DialogueBox.Click += DialogueBox_Click;
            }
        }

        private void picCup()
        {
            Cup = new PictureBox();
            Cup.Width = 70;
            Cup.Height = 80;
            Cup.Location = new Point(770, 470);
            Cup.Click += cup_click;
            Cup.BackColor = Color.Transparent;
            Controls.Add(Cup);
        }
        private void cup_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (cup == false)                              //When user doesn't posses a cup, give cup
            {
                Dialogue();
                DialogueBox.Text = "You obtained a cup!";
                ItemBox.Items.Add("Cup");
                DialogueBox.Click += DialogueBox_Click;
                cup = true;
            }
            else if (cup == true)                         //Prevents user from obtaining another cup
            {
                Dialogue();
                DialogueBox.Text = "Nothing is on the drawer.";
                DialogueBox.Click += DialogueBox_Click;
            }
        }

        private void picExitPortal()
        {
            ExitPortal = new PictureBox();
            ExitPortal.Width = 60;
            ExitPortal.Height = 70;
            ExitPortal.Location = new Point(350, 295);
            ExitPortal.BackColor = Color.Transparent;
            ExitPortal.Click += ExitPortal_click;
            Controls.Add(ExitPortal);
        }

        private void ExitPortal_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (levelOneKey == false)                            //When user doesn't posses a key item
            {
                Dialogue();
                DialogueBox.Text = "Need Key to insert.";
                DialogueBox.Click += DialogueBox_Click;
            }
            else if (levelOneKey == true)                        //When user have key, unlocks door
            {
                Dialogue();
                DialogueBox.Text = "Cell Door Unlocked.";
                DialogueBox.Click += DialogueBox_Click;
                cellDoorUnlock = true;
            }    
        }

        private void picCellDoor()
        {
            CellDoor = new PictureBox();
            CellDoor.Width = 470;
            CellDoor.Height = 740;
            CellDoor.Location = new Point(520, 50);
            CellDoor.BackColor = Color.Transparent;
            CellDoor.Click += CellDoor_click;
            Controls.Add(CellDoor);
        }
        private void CellDoor_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (cellDoorUnlock == false)
            {
                Dialogue();
                DialogueBox.Text = "The cell door is locked.";
                DialogueBox.Click += DialogueBox_Click;
            }
            else if(cellDoorUnlock == true)
            {
                currentBackground = backbuffer3;
                Invalidate();
                background_Change();
            }          
        }

        //Level 2 Clickable Item
        private void picElectricLock()
        {
            ElectricLock = new PictureBox();
            ElectricLock.Width = 100;
            ElectricLock.Height = 95;
            ElectricLock.Location = new Point(350, 305);
            ElectricLock.BackColor = Color.Transparent;
            ElectricLock.Click += ElectricLock_click;
            Controls.Add(ElectricLock);
        }
        private void ElectricLock_click(object sender, EventArgs e)
        {
           //Sara work on inserting textbox and check if user input is correct
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawBackground(g, currentBackground);                   //Send and receive info from drawbackground about what background to paint

            //Never forget invalidate !!!!!!!!
        }

        private void drawBackground(Graphics g, Bitmap background)
        {
            //Derfault background is backbuffer1 unless user move between rooms
            g.DrawImage(background, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            if (background == backbuffer1)
            {
                if(hideSprite == false)             //Sprite will be on display UNTIL user spill water to npc and make the sprite leave. (hidesprite statement becomes true)
                {
                    g.DrawImage(sprite, rectDest);
                }
                spriteClick();
                picCellDoor();
                picExitPortal();
            }    
        }   
        private void picArrowL_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                        //For better UX, if user move between rooms without removing dialogue yet it automatically does
            if(cellDoorUnlock == false)                          //For level 1, when cell door is still locked
            {
                currentBackground = backbuffer2;
                background_Change();
            }
            else if(cellDoorUnlock == true)
            {

            }
          
        }

        private void picArrowR_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (cellDoorUnlock == false)
            {
                currentBackground = backbuffer1;
                background_Change();
            }
            else if (cellDoorUnlock == true)
            {

            }
        }

        private void background_Change()
        {
            Controls.Clear();                                 //Remove picCellDoor and picExitPortal from background. 
            Controls.Add(picArrowL);                          //Tho it removes every controls so re-add essential controls
            Controls.Add(picArrowR);                          //Note that Controls.Remove() DOES NOT WORk because it is connected to paintEvent
            Controls.Add(ItemBox);

            if (currentBackground == backbuffer1)             //Items that goes along with background1
            {
                spriteClick();
                picCellDoor();
                picExitPortal();
            }
            else if (currentBackground == backbuffer2)        //Items that goes along with background2
            {
                picBeds();
                picfaucets();
                picLightBulb();
                picTowel();
                picCabinet();
                picCup();
            }
            else if (currentBackground == backbuffer3)
            {
                picElectricLock();
            }

            Invalidate();                //Force the form to RE-DRAW
        }
    }
}
//Goals
//Spliting dialogue text into two sequence...
//Inserting text box for user answers
//How to make arror count background and move between rooms
//Character idle (If time allows)

