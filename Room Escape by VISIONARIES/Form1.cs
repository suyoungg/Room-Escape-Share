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
        List<string> currentDialogueLine;   //List to store many dialogue lines in sequence
        int currentDialogueIndex;           //To track dialogue and display


        Label npcTitle;


        //Cliackable item as picbox
        public PictureBox Beds;
        public PictureBox faucets;
        PictureBox LightBulb;
        PictureBox Mirror;
        public PictureBox Towel;
        PictureBox Cup;
        PictureBox ExitPortal;
        PictureBox CellDoor;
        public PictureBox Cabinet;

        //Level2
        PictureBox ElectricLock;
        PictureBox Vent;
        ListBox RiddleBox;
        string phrase;
        string NewPhrase;
        TextBox LockBox;


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
        bool hideSprite = false;               //Bool for hiding/showing sprite NPC

        //Counters
        int countBed = 0;                 
        int countCabinet = 0;
        int countTowel = 0;
        int dialogueCount = 0;

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Make every background image full screen size
            this.WindowState = FormWindowState.Maximized;
            backbuffer1 = new Bitmap(frmG.picBack1.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer3 = new Bitmap(frmG.picBack3.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer4 = new Bitmap(frmG.picBack4.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer5 = new Bitmap(frmG.picBack5.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            currentBackground = backbuffer1;                                          //Default background is backbuffer1. Later to be useful in Paint event for changing background


            //Whenever Dialogue needed, call Dialogue Procedure and write apropriate text. Must call the click event to make sure it is removed after!
            currentDialogueLine = new List<string>();
            currentDialogueIndex = 0;         
            Dialogue();
            DialogueBox.Text = "You were framed for a crime and are sentenced to life in prison.\nYou don't think you deserve this. It's time to escape.";

            //Item Box
            Items();
            ItemBox.Items.Add("ITEM BOX");

            //Arrow location
            int arrowX = Screen.PrimaryScreen.Bounds.Width - 110;
            int arrowY = Screen.PrimaryScreen.Bounds.Height - 150;
            int arrowXL = Screen.PrimaryScreen.Bounds.Width - 1280; //1380 for school comp
            picArrowR.Location = new Point(arrowX, arrowY);                                       //Location for arrow Right
            picArrowL.Location = new Point(arrowXL, arrowY);                                      //Location for arrow Left

            // Warden sprite location in background 2
            int spriteX = Screen.PrimaryScreen.Bounds.Width - characSize;
            int spriteY = Screen.PrimaryScreen.Bounds.Height - 650;
            sprite = new Bitmap(frmG.picSprite.Image, characSize, characSize);
            rectDest = new Rectangle(spriteX, spriteY, characSize, characSize);

            //Emma NPC
            sprite2 = new Bitmap(frmG.picEmma.Image, characSize, characSize);
            rectD = new Rectangle(800, 200, characSize, characSize);

            //Riddler NPC
            sprite3 = new Bitmap(frmG.picRiddler.Image, characSize, characSize);
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
            DialogueBox.Click += DialogueBox_Click;
            Controls.Add(DialogueBox);                                             //This adds the label to the controls.
            DialogueBox.BringToFront();                                            //Bring diologue on top of every layers so picbox don't go over dialogue   
        }

        private void DialogueBox_Click(object sender, EventArgs e)                 //When user click dialogue box,
        {
            Controls.Remove(npcTitle);
            ShowNextLine();
            
        }
        private void ShowNextLine()            
        {
           if(currentDialogueIndex < currentDialogueLine.Count)                 //As long as current index is smaller than the amount of dialogue prepared
            {
                DialogueBox.Text = currentDialogueLine[currentDialogueIndex];   //Display dialogue line in certain index
                currentDialogueIndex++;                                         //Increases so dialogue doesn't repeat same line
            }
           else                                                                 //If it's just one sentence then just remove the dialogue box in a click
            {
                Controls.Remove(npcTitle);
                Controls.Remove(DialogueBox);
            }
        }
        //Since Bitmap don't have click event, put a transparent picbox on top of bitmap so user can click it for interaction
        private void wardenClick()
        {
            clickSprite = new PictureBox();
            clickSprite.Width = 150;
            clickSprite.Height = 500;
            clickSprite.Location = new Point(1050, 300);
            clickSprite.BackColor = Color.Transparent;               //This allows picbox to be transparent
            clickSprite.Click += warden_Click;                  //Can interact with NPC through a click event
            Controls.Add(clickSprite);
        }

        //Name tag on NPC
        private void NpcTitle()
        {
            npcTitle = new Label();
            npcTitle.Width = 250;
            npcTitle.Height = 40;
            npcTitle.Location = new Point(285, 610);
            npcTitle.BackColor = Color.FromArgb(200, Color.Black);
            npcTitle.ForeColor = Color.YellowGreen;
            npcTitle.Font = new Font("Courier New", 15, FontStyle.Regular);
            npcTitle.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(npcTitle);
            npcTitle.BringToFront();
        }

        //When NPC warden is clicked
        private void warden_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                            //Remove any previous dialogue before opening new dialogue
            if (cupFilledWater == false)                             //If user don't posses an item called cup filled with water
            {
                Dialogue();
                NpcTitle();
                npcTitle.Text = "Officer French"; ;
                DialogueBox.Text = "What are you looking at? Go back to your cell. \n God, this place is so dirty, I want to get out of here right now!";                              
            }
            else if (cupFilledWater == true && levelOneKey == false)                         //If user posses cup filled with water
            {
                currentDialogueLine = new List<string>
                {
                    "What have you done! You spilled water on me.\nNow I have to go change my clothes.",   //index = 0;
                    "*Leaves room and drops key*",                                 
                    "You obtained the MASTER KEY"
                };
                currentDialogueIndex = 0;
                NpcTitle();
                npcTitle.Text = "Officer French";
                Dialogue();
                ShowNextLine();

                levelOneKey = true;                                  //Posses Key to escape level 1
                ItemBox.Items.Remove("Cup filled with water");       //Remove item after it is USED
                ItemBox.Items.Add("Master Key");                     //Obtain different item 
                hideSprite = true;                                   //Refer frmPaint event. program paints sprite ONLY hidesprite == false. When hidesprite == true it won't draw the sprite.
                Invalidate();                                        //Always repaint after paint event!
            }
        }


        private void picEmmaLocked()
        {
            clickSprite = new PictureBox();
            clickSprite.Width = 150;
            clickSprite.Height = 470;
            clickSprite.Location = new Point(850, 220);
            clickSprite.BackColor = Color.Transparent;            
            clickSprite.Click += EmmaLocked_Click;                  
            Controls.Add(clickSprite);
        }
       

        //When NPC image is clicked
        private void EmmaLocked_Click (object sender, EventArgs e)
        {
          
            currentDialogueLine = new List<string>
            {
               "Hey! Please don't leave yet!",
               "I've been stuck here for too long! \n I'll help you escape if you let me come with you.\n I have the item you need to escape this floor.",
               "So...Could you maybe...Unlock my cell? \n The lock is on the side and you'll need a password."
            };
            currentDialogueIndex = 0;
            NpcTitle();
            npcTitle.Text = "Emma Gibbs";
            Dialogue();                  //When user click on dialogue after the first message, they initiate dialogue_click which calls ShowNextLine and it display next message
            ShowNextLine();
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
                countBed++;
            }
            else if (countBed > 0)                                                       //When it's second time clicking picBeds (After they obtained faucets)
            {
                Dialogue();
                DialogueBox.Text = "No time to sleep. It's time to escape.";
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
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                countCabinet++;
            }
            else if (countCabinet > 0)
            {
                Dialogue();
                DialogueBox.Text = "Cabinet is empty.";
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
            }
            else if (faucetPiece == 3 && cup == false)          //If user has faucets pieces but NO cups to fill water
            {
                Dialogue();
                DialogueBox.Text = "Faucet fixed. You hear water drops...";
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
            }
            
            //When user gathered all 3 faucet pieces AND a cup, faucet will give cup filled water
            if(faucetPiece == 3 && cup == true && cupFilledWater == false) //cupFilledWater == false is to prevent user from obtaining another cupFilledWater
            {
                Dialogue();
                DialogueBox.Text = "Cup is now filled with water.\n ...Maybe you could spill this on someone.";
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
        }
        private void picMirror()
        {
            Mirror = new PictureBox();
            Mirror.Width = 200;
            Mirror.Height = 200;
            Mirror.Location = new Point(1120, 270);
            Mirror.Click += Mirror_click;
            Mirror.BackColor = Color.Transparent;
            Controls.Add(Mirror);
        }
        private void Mirror_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "You see your face reflected in the mirror. You could look better.";
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
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                countTowel++;
            }
            
             else if (countTowel > 0)
            {
                Dialogue();
                DialogueBox.Text = "Dry towel.";
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
                cup = true;
            }
            else if (cup == true)                         //Prevents user from obtaining another cup
            {
                Dialogue();
                DialogueBox.Text = "Nothing is on the drawer.";
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
            }
            else if (levelOneKey == true)                        //When user have key, unlocks door
            {
                Dialogue();
                DialogueBox.Text = "Cell Door Unlocked.";
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
            //Controls.Remove(DialogueBox);
            //if (cellDoorUnlock == false)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == true)
            //{
            //    currentBackground = backbuffer3;
            //    Invalidate();
            //    background_Change();
            //}

            //Quick Route to level 2
            Controls.Remove(DialogueBox);
            if (cellDoorUnlock == true)
            {
                Dialogue();
                DialogueBox.Text = "The cell door is locked.";
            }
            else if (cellDoorUnlock == false)
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
            picArrowL.Enabled = false;
            picArrowR.Enabled = false;
            Vent.Enabled = false;
            clickSprite.Enabled = false;
            Riddle();
            Lock();
            
        }
        private void Riddle()
        {
            RiddleBox = new ListBox();
            RiddleBox.Width = 500;
            RiddleBox.Height = 150;
            RiddleBox.Location = new Point(540, 500);
            RiddleBox.BackColor = Color.Black;
            RiddleBox.ForeColor = Color.YellowGreen;
            RiddleBox.Font = new Font("Century", 15, FontStyle.Regular);
            RiddleBox.BorderStyle = BorderStyle.FixedSingle;
            RiddleBox.Items.Add("          If I stab someone else, I die. What am I?");
            Controls.Add(RiddleBox);
            RiddleBox.BringToFront();
        }

        private void Lock()
        {
            LockBox = new TextBox();
            LockBox.Width = 500;
            LockBox.Height = 400;
           // LockBox.Location = new Point(555, 505);
            LockBox.Location = new Point(540, 675);
            LockBox.BackColor = Color.Black;
            LockBox.ForeColor = Color.YellowGreen;
            LockBox.Font = new Font("Seif", 12, FontStyle.Regular);
            LockBox.BorderStyle = BorderStyle.FixedSingle;
            LockBox.Text = "";

            Controls.Add(LockBox);
            LockBox.KeyDown += answer_enter;
            LockBox.BringToFront();

        }
        private void answer_enter(object sender, KeyEventArgs e)
        {
            phrase = LockBox.Text;
            NewPhrase = phrase.Replace("b", "B");

            if (e.KeyCode == Keys.Enter)
            {
                Controls.Remove(LockBox);
                Controls.Remove(RiddleBox);

                if (NewPhrase == "Bee" || NewPhrase == "A bee")
                {
                    Dialogue();
                    DialogueBox.Text = "Correct.";
                    currentBackground = backbuffer4;
                    picArrowL.Enabled = true;
                    picArrowR.Enabled = true;
                    ItemBox.Items.Add("Master Key");             //Gets removed after changing to new background so re-add
                    background_Change();

                }
                else
                {
                    Dialogue();
                    DialogueBox.Text = "Incorrect, try again.";
                }
            }
        }


        private void picVent()
        {
            Vent = new PictureBox();
            Vent.Width = 195;
            Vent.Height = 110;
            Vent.Location = new Point(265, 540);
            Vent.BackColor = Color.Transparent;
            Vent.Click += Vent_click;
            Controls.Add(Vent);
        }
        private void Vent_click(object sender, EventArgs e)
        {
            Dialogue();
            DialogueBox.Text = "You found a scribble written on vent \n Hint: Insect";
        }

        private void picEmma()
        {
            clickSprite = new PictureBox();
            clickSprite.Width = 170;
            clickSprite.Height = 500;
            clickSprite.Location = new Point(1050, 290);
            clickSprite.BackColor = Color.Transparent;
            clickSprite.Click += picEmma_click;
            Controls.Add(clickSprite);
        }
        private void picEmma_click(object sender, EventArgs e)
        {
            currentDialogueLine = new List<string>
                {
                    "Thank you for saving me! You won't regret this.",
                    "Oh the item? Um, I am too hungry to talk about that right now \n so... Maybe you can bring me food first?",
                    "My favorite food is poutine, not that I want you to make \n poutine but if you want your item..."
                };
            currentDialogueIndex = 0;
            NpcTitle();
            npcTitle.Text = "Emma Gibbs";
            Dialogue();
            ShowNextLine();
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
                wardenClick();
                picCellDoor();
                picExitPortal();
            }    
            if(background == backbuffer4)
            {
                g.DrawImage(sprite2, rectD);
            }
        }   
        private void picArrowL_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                        //For better UX, if user move between rooms without removing dialogue yet it automatically does
             if(currentBackground == backbuffer1)                          //For level 1, when cell door is still locked
            {
                currentBackground = backbuffer2;
                background_Change();
            } 
            else if (currentBackground == backbuffer4)
            {
                currentBackground = backbuffer5;
                background_Change();
            }



        }

        private void picArrowR_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (currentBackground == backbuffer2)
            {
                currentBackground = backbuffer1;
                background_Change();
            }
            else if (currentBackground == backbuffer4)
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
                wardenClick();
                picCellDoor();
                picExitPortal();
            }
            else if (currentBackground == backbuffer2)        //Items that goes along with background2
            {
                picBeds();
                picfaucets();
                picLightBulb();
                picMirror();
                picTowel();
                picCabinet();
                picCup();
            }
            else if (currentBackground == backbuffer3)
            {
                picElectricLock();
                picVent();
                picEmmaLocked();
            }
            else if (currentBackground == backbuffer4)
            {
                picEmma();
            }
            else if (currentBackground == backbuffer5)
            {

            }

            Invalidate();                //Force the form to RE-DRAW
        }
    }
}
//Goals
//How to make arrow count background and move between rooms
//File Access. Emma will give a slip of paper and user can check what item they need for poutine 
//Remove arrowR for backbuffer 1 but make it appear as it goes to backbuffer2
//Name tag problem AGAIN
//Character idle (If time allows)
//Home Display (start save and whatever)
//Character face Card if time allows

