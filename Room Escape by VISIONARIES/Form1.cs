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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Threading;
using System.Media;
using static System.Windows.Forms.AxHost;
using System.Security.Claims;

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
        Bitmap backbuffer4;
        Bitmap backbuffer5;
        Bitmap backbuffer6;
        Bitmap backbuffer7;

        //Level 3 Backgrounds
        Bitmap backbuffer8;
        Bitmap backbuffer9;
        Bitmap backbuffer10;
        Bitmap backbuffer11;
        Bitmap backbuffer12;
        Bitmap backbuffer13;

        Bitmap currentBackground;                    //When painting different backgrounds and make specific items to appear on a specific backbuffer
       
       // NPCs
        Bitmap sprite;                               //Warden
        Bitmap sprite2;                              //Emma
        Bitmap sprite3;                              //Riddler


        Label npcTitle;                              //Name tag for each NPC
        Label RoomName;                              //Room name 

        int characSize = 600;
        Rectangle rectDest, rect2, rect3;
        PictureBox clickSprite;                      //A transparent picBox that will be overlaped over NPC for click interaction
        PictureBox clickSprite2;


        //Dialogue Size and property
        Label DialogueBox;
        List<string> currentDialogueLine;            //List to store dialogues if there are multiple lines in sequence
        int currentDialogueIndex;                    //To track dialogue sequences

        //Item Box
        ListBox ItemBox;
        int faucetPiece = 0;
        int Riddles = 0;

        //Cliackable item as picbox
        public PictureBox clickableItem;
        public PictureBox Cabinet;

        //Level2
        PictureBox Vent;
        ListBox RiddleBox;
        TextBox LockBox;

        //BOOLS
        bool hideSprite = false;                            //Bool for hiding/showing sprite NPC
        bool cellDoorUnlock = false;
        bool OvenOpened = false;
        bool StoveFixed = false;
        bool dialogueComplete = false;                     //Bool to prevent question box popping same time as dialogue box
        bool hiddenSpotClicked = false;                    //To check if an item in level 3 is clicked (To open riddle box when this item is clicked)
        bool brokenWallClicked = false;
        bool WardenCloth = false;
        bool UntangleWire = false;
        bool ElectricityOn = false;

        //Counters
        int countBed = 0;                                  //Used so dialogue for 1st time click and 2nd time click on item is diff
        int countCabinet = 0;
        int countTowel = 0;


        //File Access
        string filePath = Application.StartupPath + "\\Checklist.txt";

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        { 
            //Music
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\GameMusiclvl1.wav";
            player.PlayLooping();

            //Make every background image full screen size
            this.WindowState = FormWindowState.Maximized;
            backbuffer1 = new Bitmap(frmG.picBack1.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer3 = new Bitmap(frmG.picBack3.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer4 = new Bitmap(frmG.picBack4.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer5 = new Bitmap(frmG.picBack5.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer6 = new Bitmap(frmG.picBack6.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer7 = new Bitmap(frmG.picBack7.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer8 = new Bitmap(frmG.picBack8.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer9 = new Bitmap(frmG.picBack9.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer10 = new Bitmap(frmG.picBack10.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer11 = new Bitmap(frmG.picBack11.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer12 = new Bitmap(frmG.picBack12.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            backbuffer13 = new Bitmap(frmG.picBack13.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            currentBackground = backbuffer1;                                          //Default background is backbuffer1. Later to be useful in Paint event for changing background


            //Whenever Dialogue needed, call Dialogue Procedure and write apropriate text.
            currentDialogueLine = new List<string>();
            currentDialogueIndex = 0;         
            Dialogue();
            DialogueBox.Text = "You were framed for a crime and are sentenced to life in prison.\nYou don't think you deserve this. It's time to escape.";

            //Item Box
            Items();
            ItemBox.Items.Add("ITEM BOX");
            ItemBox.SelectedIndexChanged += ItemBox_ItemClicked;                  //Event when an item is clicked from the listBox

            //Room Name
            Room_Name();
            RoomName.Text = "Cell Door";

            //Arrow location
            int arrowX = Screen.PrimaryScreen.Bounds.Width - 110;
            int arrowY = Screen.PrimaryScreen.Bounds.Height - 150;
            int arrowXL = Screen.PrimaryScreen.Bounds.Width - 1380; //1380 for school comp
            picArrowL.Location = new Point(arrowXL, arrowY);                                      //Location for arrow Left
            picArrowR.Location = new Point(arrowX, arrowY);                                       //Location for arrow Right

            // Warden sprite location in background 2
            sprite = new Bitmap(frmG.picSprite.Image, characSize, characSize);
            rectDest = new Rectangle(830, 250, characSize, characSize);

            //Emma NPC
            sprite2 = new Bitmap(frmG.picEmma.Image, characSize, characSize);
            rect2 = new Rectangle(800, 200, characSize, characSize);

            //Riddler NPC
            sprite3 = new Bitmap(frmG.picRiddler.Image, characSize, characSize);
            rect3 = new Rectangle(30, 200, 600, 600);
        }

        private void Dialogue()
        {
            //Create a new Label for Dialogue Box
            DialogueBox = new Label();
            DialogueBox.Width = 900;
            DialogueBox.Height = 200;
            DialogueBox.Location = new Point(280, 600);
           // DialogueBox.Location = new Point(240, 400);
            DialogueBox.BackColor = Color.FromArgb(200, Color.Black);              //fromArgb refers to transparency
            DialogueBox.ForeColor = Color.Beige;
            DialogueBox.Font = new Font("Courier New", 14, FontStyle.Regular);
            DialogueBox.BorderStyle = BorderStyle.FixedSingle;
            DialogueBox.TextAlign = ContentAlignment.MiddleCenter;
            DialogueBox.Click += DialogueBox_Click;
            Controls.Add(DialogueBox);                                             //This adds the label to the controls.
            DialogueBox.BringToFront();                                            //Bring diologue on top of every layers so picbox don't go over dialogue   
        }

        private void DialogueBox_Click(object sender, EventArgs e)                 //When user click dialogue box,
        {
           // Controls.Remove(npcTitle);
            ShowNextLine();                                                        //If there is a next line
        }
        private void ShowNextLine()            
        {
           if(currentDialogueIndex < currentDialogueLine.Count)                  //As long as current index is smaller than the amount of dialogue prepared
            {
                npcTitle.BringToFront();
                DialogueBox.Text = currentDialogueLine[currentDialogueIndex];    //Display dialogue line in certain index
                currentDialogueIndex++;                                          //Increases so it move on to next line
            }
           else                                                               
            {
                Controls.Remove(DialogueBox);                                    //If there is only a line to diplay dialogue disappears in a click
                Controls.Remove(npcTitle);
                dialogueComplete = true;

                if(brokenWallClicked == true)
                {
                    currentBackground = backbuffer8;
                    Invalidate();
                    background_Change();
                }

                if (hiddenSpotClicked == true)
                {
                    if (Riddles == 0)
                    {
                        Riddle();
                        RiddleBox.Items.Add("    How many months have I been in prison?");
                        Lock();
                    }
                    else if (Riddles == 1)
                    {
                        Riddle();
                        RiddleBox.Items.Add("    What becomes smaller when you flip it upside down?");
                        Lock();
                    }
                    else if (Riddles == 2)
                    {
                        Riddle();
                        RiddleBox.Items.Add("    What is the name of this prison?");
                        Lock();
                    }
                }

            }
        }

        //Name tag on NPC
        private void NpcTitle()
        {
            npcTitle = new Label();
            npcTitle.Width = 250;
            npcTitle.Height = 40;
            npcTitle.Location = new Point(285, 610);
           // npcTitle.Location = new Point(255, 410);
            npcTitle.BackColor = Color.FromArgb(200, Color.Black);
            npcTitle.ForeColor = Color.YellowGreen;
            npcTitle.Font = new Font("Courier New", 15, FontStyle.Regular);
            npcTitle.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(npcTitle);
            npcTitle.BringToFront();
        }

        private void Room_Name()
        {
            RoomName = new Label();
            RoomName.Width = 150;
            RoomName.Height = 40;
            RoomName.Location = new Point(1200, 35);
          //  RoomName.Location = new Point(1100, 35);
            RoomName.BackColor = Color.FromArgb(128, Color.Black);
            RoomName.ForeColor = Color.Beige;
            RoomName.Font = new Font("Courier New", 12, FontStyle.Regular);
            RoomName.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(RoomName);
            RoomName.BringToFront();
        }

        //User Item Box
        private void Items()
        {
            ItemBox = new ListBox();
            ItemBox.Width = 200;
            ItemBox.Height = 150;
            ItemBox.Location = new Point(80, 20);
            ItemBox.BackColor = Color.Black;
            ItemBox.ForeColor = Color.Beige;
            ItemBox.Font = new Font("Courier New", 12, FontStyle.Regular);
            ItemBox.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(ItemBox);
        }

        //Since Bitmap don't have click event, put a transparent picbox on top of bitmap so user can click it for interaction
        private void wardenClick()
        {
            clickSprite = new PictureBox();
            clickSprite.Width = 150;
            clickSprite.Height = 500;
            clickSprite.Location = new Point(1050, 300);
            clickSprite.BackColor = Color.Transparent;               //This allows picbox to be transparent
            clickSprite.Click += warden_Click;                       //Can interact with NPC through a click event
            Controls.Add(clickSprite);
        }

        //When NPC warden is clicked
        private void warden_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                           //Remove any previous dialogue before opening new dialogue
            Controls.Remove(npcTitle);

            if (!ItemBox.Items.Contains("Cup with water") && !ItemBox.Items.Contains("Master Key"))          //If user don't posses an item called cup with water
            {
                Dialogue();
                NpcTitle();
                npcTitle.Text = "Officer French"; ;
                DialogueBox.Text = "What are you looking at? Go back to your cell. \n God, this place is so dirty, I want to get out of here right now!";                              
            }
            else if (ItemBox.Items.Contains("Cup with water") && !ItemBox.Items.Contains("Master Key"))      //If user posses cup with water + !ItemBox.Items.Contains("Master Key") is to prevent from obtaining 2 items
            {
                currentDialogueLine = new List<string>
                {
                    "What have you done! You spilled water on me.\nNow I have to go change my clothes.",   //index = 0;
                    "*Leaves room and drops key*",                                                         //index = 1;
                    "You obtained the MASTER KEY"                                                          //index = 2;
                };
                currentDialogueIndex = 0;
                NpcTitle();
                npcTitle.Text = "Officer French";
                Dialogue();
                ShowNextLine();
                          
                ItemBox.Items.Remove("Cup with water");              //Remove item after USED
                ItemBox.Items.Add("Master Key");                     //Posses Key to escape level 1
                hideSprite = true;                                   //Refer frmPaint event. program paints sprite ONLY hidesprite == false. When hidesprite == true it won't draw the sprite.
                Invalidate();                                        //Always repaint after paint event!
            }
        }

        //Npc (Locked image version)
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
            Controls.Remove(DialogueBox);
            Controls.Remove(npcTitle);
            currentDialogueLine = new List<string>
            {
               "Hey! Please don't leave yet!",
               "I've been stuck here for too long! \n I'll help you escape if you let me come with you.\n I have the item you need to escape this floor.",
               "Can you Unlock my cell? \n The lock is on the side and you'll need a password."

            };
            currentDialogueIndex = 0;
            NpcTitle();
            npcTitle.Text = "Emma Gibbs";
            Dialogue();                  //When user click on dialogue after the first message, they initiate dialogue_click which calls ShowNextLine and it display next message
            ShowNextLine();
        }

       

        //CLICKABLE ITEMS
        public void picBeds()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 560;
            clickableItem.Height = 390;
            clickableItem.Location = new Point(65, 390);
            clickableItem.Click += beds_click;                                             //Interaction when Bed is clicked
            clickableItem.BackColor = Color.Transparent;                                   //Make it transparent so when user click bed they are actually clicking this picbox
            Controls.Add(clickableItem);
        }

        public void beds_click(object sender, EventArgs e)                        //Click Event. When user click picpeds dialogue box appear
        {
            Controls.Remove(DialogueBox);                                         //Remove any previously opened dialogue

            if (countBed == 0)
            {
                Dialogue();
                DialogueBox.Text = "Found a piece of faucet from the beds.";
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");           //Remove ANY previous faucet line so we don't have unecessary repeatings but instead merge texts together
                faucetPiece++;
                ItemBox.Items.Add($"Faucet Piece {faucetPiece}/3");
                countBed++;                                                      //Help program recognize first/second time clicking an item
            }
            else if (countBed > 0)                                               //When it's second time clicking picBeds (After they obtained faucets)
            {
                Dialogue();
                DialogueBox.Text = "No time to sleep. It's time to escape.";
            }
        }

        public void picCabinet()                                                    //Repeat same procedure as picBeds
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 172;
            clickableItem.Height = 170;
            clickableItem.Location = new Point(773, 280);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Cabinet_Click;
            Controls.Add(clickableItem);
        }

        public void Cabinet_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);

            if (countCabinet == 0)
            {
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
            clickableItem = new PictureBox();
            clickableItem.Width = 200;
            clickableItem.Height = 220;
            clickableItem.Location = new Point(1120, 470);
            clickableItem.Click += faucets_click;
            clickableItem.BackColor = Color.Transparent;
            Controls.Add(clickableItem);
        }

        private void faucets_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);

            if (faucetPiece < 3)                                                 //If user hasn't gathered all faucet pieces
            {
                Dialogue();
                DialogueBox.Text = "The faucet is broken. \n You might need to find missing pieces to fix.";
            }
            else if (faucetPiece == 3 && !ItemBox.Items.Contains("Cup"))          //If user has faucets pieces but NO cup
            {
                Dialogue();
                DialogueBox.Text = "Faucet fixed. You hear water drops...";
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
            }
            
            //When user gathered all 3 faucet pieces AND a cup, faucet will give cup filled water
            if(faucetPiece == 3 && ItemBox.Items.Contains("Cup") && !ItemBox.Items.Contains("Cup with water")) //cupFilledWater == false is to prevent user from obtaining another cupFilledWater
            {
                Dialogue();
                DialogueBox.Text = "Cup is now filled with water.\n ...Maybe you could spill this on someone.";
                ItemBox.Items.Remove("Cup");
                ItemBox.Items.Remove($"Faucet Piece {faucetPiece}/3");
                ItemBox.Items.Add("Cup with water");
            }       
        }

        private void picLightBulb()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 140;
            clickableItem.Height = 100;
            clickableItem.Location = new Point(650, 20);
            clickableItem.Click += LightBulb_click;
            clickableItem.BackColor = Color.Transparent;
            Controls.Add(clickableItem);
        }     
        private void LightBulb_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "Light Bulb looks rusty and old.";
        }
        private void picMirror()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 200;
            clickableItem.Height = 200;
            clickableItem.Location = new Point(1120, 270);
            clickableItem.Click += Mirror_click;
            clickableItem.BackColor = Color.Transparent;
            Controls.Add(clickableItem);
        }
        private void Mirror_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "You see your face reflected in the mirror. You could look better.";
        }

        private void picTowel()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 70;
            clickableItem.Height = 100;
            clickableItem.Location = new Point(770, 570);
            clickableItem.Click += towel_click;
            clickableItem.BackColor = Color.Transparent;
            Controls.Add(clickableItem);
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
            clickableItem = new PictureBox();
            clickableItem.Width = 70;
            clickableItem.Height = 80;
            clickableItem.Location = new Point(770, 470);
            clickableItem.Click += cup_click;
            clickableItem.BackColor = Color.Transparent;
            Controls.Add(clickableItem);
        }
        private void cup_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Cup"))                              //When user doesn't posses a cup, give cup
            {
                Dialogue();
                DialogueBox.Text = "You obtained a cup!";
                ItemBox.Items.Add("Cup");
            }
            else if (ItemBox.Items.Contains("Cup"))                         //Prevents user from obtaining another cup
            {
                Dialogue();
                DialogueBox.Text = "Nothing is on the drawer.";
            }
        }

        private void picExitPortal()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 60;
            clickableItem.Height = 70;
            clickableItem.Location = new Point(350, 295);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += ExitPortal_click;
            Controls.Add(clickableItem);
        }

        private void ExitPortal_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Master Key"))                            //When user doesn't posses a key item
            {
                Dialogue();
                DialogueBox.Text = "Need Key to insert.";
            }
            else if (ItemBox.Items.Contains("Master Key"))                        //When user have key, unlocks door
            {
                Dialogue();
                DialogueBox.Text = "Cell Door Unlocked.";
                cellDoorUnlock = true;                                            //When this bool is true, clicking cellDoor will allow user move onto level 2
            }    
        }

        private void picCellDoor()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 470;
            clickableItem.Height = 740;
            clickableItem.Location = new Point(520, 50);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += CellDoor_click;
            Controls.Add(clickableItem);
        }
        private void CellDoor_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            //if (cellDoorUnlock == false)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == true)
            //{
            //    ItemBox.Items.Remove("Master Key");
            //    currentBackground = backbuffer3;                 //Change background to level 2
            //    Invalidate();                                    //Always Invalidate when changing backgrounds
            //    background_Change();                             //To call items that are in level 2 room 1
            //}

            //Quick Route to level 2
            //if (cellDoorUnlock == true)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == false)
            //{
            //    ItemBox.Items.Remove("Master Key");
            //    currentBackground = backbuffer3;
            //    Invalidate();
            //    background_Change();
            //}

            //Quick Route to level 3
            //if (cellDoorUnlock == true)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == false)
            //{
            //    ItemBox.Items.Remove("Master Key");
            //    currentBackground = backbuffer9;
            //    Invalidate();
            //    background_Change();
            //}

            //Quick Route to CCTV ROOM
            //if (cellDoorUnlock == true)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == false)
            //{
            //    ItemBox.Items.Remove("Master Key");
            //    currentBackground = backbuffer10;
            //    Invalidate();
            //    background_Change();
            //}

            //Quick Route to Clothing ROOM
            //if (cellDoorUnlock == true)
            //{
            //    Dialogue();
            //    DialogueBox.Text = "The cell door is locked.";
            //}
            //else if (cellDoorUnlock == false)
            //{
            //    ItemBox.Items.Remove("Master Key");
            //    currentBackground = backbuffer12;
            //    Invalidate();
            //    background_Change();
            //}

            ////Quick Route to Staff room
            if (cellDoorUnlock == true)
            {
                Dialogue();
                DialogueBox.Text = "The cell door is locked.";
            }
            else if (cellDoorUnlock == false)
            {
                ItemBox.Items.Remove("Master Key");
                currentBackground = backbuffer13;
                Invalidate();
                background_Change();
            }
        }

        //File Acces for level 2
        private void fileAccess()
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                StreamReader myFile = new StreamReader(fs);
                string fileContents = myFile.ReadToEnd();
                MessageBox.Show(fileContents);
                myFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                                 //Recipe shows up
            }
        }
        private void ItemBox_ItemClicked(object sender, EventArgs e)
        {
            if(ItemBox.SelectedItem != null && ItemBox.SelectedItem.ToString() == "Poutine Recipe")
            {   // != null checks if item is selected from the listbox (null means there is no reference)
                fileAccess();
            }
        }

        //Level 2 Clickable Item
        private void picElectricLock()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 100;
            clickableItem.Height = 95;
            clickableItem.Location = new Point(350, 305);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += ElectricLock_click;
            Controls.Add(clickableItem);
        }
        private void ElectricLock_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            picArrowL.Enabled = false;                               //Until they enter something none of them are enabled
            picArrowR.Enabled = false;
            clickableItem.Enabled = false;
            clickSprite.Enabled = false;

            Riddle();                                                //Question Box
            RiddleBox.Items.Add("             12 x 12 = 9");
            RiddleBox.Items.Add("             23 x 23 = 16");
            RiddleBox.Items.Add("             34 x 34? = ?");
            Lock();                                                  //txtBox for user input
            
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
            LockBox.KeyDown += answer_enter;                       //Calls enter key event
            LockBox.BringToFront();

        }
        private void answer_enter(object sender, KeyEventArgs e)
        {
            int answer;                                            //User Input

            if (e.KeyCode == Keys.Enter)                           //Checks answer from pressing enter
            {
                Controls.Remove(LockBox);                          //Question and answer box gone after typing ans
                Controls.Remove(RiddleBox);

                if (currentBackground == backbuffer3)              //For unlocking Emma Gibbs NPC
                {
                    if (int.TryParse(LockBox.Text, out answer))
                    {
                        if (answer == 13)
                        {
                            Dialogue();
                            DialogueBox.Text = "Correct.";
                            picArrowL.Enabled = true;                  //Enable arrow buttons after
                            picArrowR.Enabled = true;
                            clickSprite.Enabled = true;
                            currentBackground = backbuffer4;
                            background_Change();                       //Change to the background where npc is now freed
                        }
                        else
                        {
                            clickableItem.Enabled = true;
                            clickSprite.Enabled = true;
                            Dialogue();
                            DialogueBox.Text = "Incorrect, try again.";
                        }
                    }
                    else
                    {
                        clickableItem.Enabled = true;
                        clickSprite.Enabled = true;
                        Dialogue();
                        DialogueBox.Text = "Incorrect, try again.";
                    }
                }
                else if (currentBackground == backbuffer5)                //For opening oven
                {
                    if (int.TryParse(LockBox.Text, out answer))
                    {
                        if (answer == 23045)
                        {
                            Dialogue();
                            DialogueBox.Text = "Oven is open now.";
                            OvenOpened = true;
                        }
                        else
                        {
                            Dialogue();
                            DialogueBox.Text = "Incorrect, try again.";
                        }
                    }
                    else
                    {
                        Dialogue();
                        DialogueBox.Text = "Incorrect, try again.";
                    }
                }
                else if(currentBackground == backbuffer8)
                {
                    if(Riddles == 0)
                    {
                        if (int.TryParse(LockBox.Text, out answer))
                        {
                            if (answer == 40)
                            {
                                Dialogue();
                                DialogueBox.Text = "Correct! Next question.";
                                Riddles++;
                            }
                            else

                            {
                                Dialogue();
                                DialogueBox.Text = "Incorrect, try again.";
                            }
                        }
                    }
                    else if(Riddles == 1)
                    {
                        if (int.TryParse(LockBox.Text, out answer))
                        {
                            if (answer == 9)
                            {
                                Dialogue();
                                DialogueBox.Text = "Correct! Next question.";
                                Riddles++;
                            }
                            else

                            {
                                Dialogue();
                                DialogueBox.Text = "Incorrect, try again.";
                            }
                        }
                    }
                    else if (Riddles == 2)
                    {
                        string ans;
                        ans = LockBox.Text.ToString().ToUpper();
                        if (ans == "SHERWOOD")
                        {
                            Dialogue();
                            DialogueBox.Text = "Correct!";
                            Riddles++;                                        //Escape from the loop of question 3 repeating
                            currentBackground = backbuffer9;
                            Invalidate();
                            background_Change();
                        }
                        else

                        {
                            Dialogue();
                            DialogueBox.Text = "Incorrect, try again.";
                        }
                    }
                }
            }
        }

        private void picVent()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 195;
            clickableItem.Height = 110;
            clickableItem.Location = new Point(265, 540);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Vent_click;
            Controls.Add(clickableItem);
        }
        private void Vent_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "You found a scribble written on the vent \n Hint: If C + D = ABC, \n then A + B + C = answer ";
            //34 x 34 = 1156, 1 + 1 + 5 + 6 = 13
        }

        private void picEmma()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 170;
            clickableItem.Height = 500;
            clickableItem.Location = new Point(1050, 290);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += picEmma_click;
            Controls.Add(clickableItem);
        }
        private void picEmma_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);


            if (!ItemBox.Items.Contains("Poutine") && !ItemBox.Items.Contains("Poutine Recipe"))                     //if User didn't completed npc's quest yet
            {
                currentDialogueLine = new List<string>
                {
                    "Thank you for saving me! You won't regret this.",
                    "Oh the item? I'm too hungry to talk about that \n right now. Bring me food first?",
                    "My favorite food is poutine.\n Make me one if you want your item...",
                    "*You have obtained a poutine recipe.*"
                };
                currentDialogueIndex = 0;
                NpcTitle();
                npcTitle.Text = "Emma Gibbs";
                Dialogue();
                ShowNextLine();
                ItemBox.Items.Add("Poutine Recipe");

            } 
            else if (ItemBox.Items.Contains("Poutine"))               //After they completed the quest
            {
                if(!ItemBox.Items.Contains("Spoon"))
                {
                    currentDialogueLine = new List<string>
                    {
                    "Um...This poutine taste amazing!",
                    "Here I'll give you SPOON. \n You can use this to dig through something...",
                    };
                    currentDialogueIndex = 0;
                    NpcTitle();
                    npcTitle.Text = "Emma Gibbs";
                    Dialogue();
                    ShowNextLine();
                    ItemBox.Items.Add("Spoon");
                }
                else if (ItemBox.Items.Contains("Spoon"))
                {             
                    Dialogue();
                    NpcTitle();
                    npcTitle.Text = "Emma Gibbs";
                    DialogueBox.Text = "One of the room here has a route to the next floor. \n You can use the spoon to use the route.";
                } 
            }
        }


        private void picOven()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 615;
            clickableItem.Height = 298;
            clickableItem.Location = new Point(560, 90);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Oven_click;
            Controls.Add(clickableItem);
        }
        private void Oven_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Controls.Remove(LockBox);                                   // if user clicks oven multiple times, this prevents question appearing more than once     
            Controls.Remove(RiddleBox);

            if (OvenOpened == false)
            {
                Riddle();
                RiddleBox.Items.Add("             Pleas enter password to use the oven.");
                Lock();

            } else if(OvenOpened == true && !ItemBox.Items.Contains("Frozen Cheese"))                 //If oven is fixed but user doesn't have cheese
            {
                Dialogue();
                DialogueBox.Text = "You have nothing to unfreeze.";
            }
             else if (OvenOpened == true && ItemBox.Items.Contains("Frozen Cheese"))                 //If oven is fixed and user have cheese
            {
                ItemBox.Items.Remove("Frozen Cheese");
                ItemBox.Items.Add("Cheese");
                Dialogue();
                DialogueBox.Text = "You have unfrozed the cheese.";
            } 
        }

        private void picKnife()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 40;
            clickableItem.Height = 90;
            clickableItem.Location = new Point(395, 150);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Knife_click;
            Controls.Add(clickableItem);
        }
        private void Knife_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Knife"))
            {
                Dialogue();
                DialogueBox.Text = "You obtained a knife.";
                ItemBox.Items.Add("Knife");
            }
            else if (ItemBox.Items.Contains("Knife"))
            {
                Dialogue();
                DialogueBox.Text = "You already obtained a knife.";
            }  

        }
        private void picPan()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 70;
            clickableItem.Height = 130;
            clickableItem.Location = new Point(1220, 210);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Pan_click;
            Controls.Add(clickableItem);
        }
        private void Pan_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Pan"))
            {
                Dialogue();
                DialogueBox.Text = "You obtained a pan.";
                ItemBox.Items.Add("Pan");
            }
            else if (ItemBox.Items.Contains("Pan"))
            {
                Dialogue();
                DialogueBox.Text = "You see nothing to collect.";
            }
          
        }
        private void picPlate()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 90;
            clickableItem.Height = 90;
            clickableItem.Location = new Point(160, 380);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Plate_click;
            Controls.Add(clickableItem);
        }
        private void Plate_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "A clean plate.";
        }

        private void picCupBoard()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 90;
            clickableItem.Height = 160;
            clickableItem.Location = new Point(1340, 530);
           // clickableItem.Location = new Point(1050, 500);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += cupBoard_click;
            Controls.Add(clickableItem);
        }

        private void cupBoard_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Gravy Sauce"))
            {
                Dialogue();
                DialogueBox.Text = "You obtained Gravy Sauce";
                ItemBox.Items.Add("Gravy Sauce");
            }
            else if (ItemBox.Items.Contains("Gravy Sauce"))
            {
                Dialogue();
                DialogueBox.Text = "Cupboard is empty.";
            }
        }


        private void picStove()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 600;
            clickableItem.Height = 80;
            clickableItem.Location = new Point(720, 400);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Stove_click;
            Controls.Add(clickableItem);
        }
        private void Stove_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if(StoveFixed == false)
            {
                Dialogue();
                DialogueBox.Text = "The stove is broken. Must fix to make poutine.";
                //Another spot the difference game
            }
        }
        private void picCuttingBoard()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 190;
            clickableItem.Height = 80;
            clickableItem.Location = new Point(400, 450);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += cuttingBoard_click;
            Controls.Add(clickableItem);
        }
        private void cuttingBoard_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);

            if (ItemBox.Items.Contains("Potato") && ItemBox.Items.Contains("Canola Oil") && ItemBox.Items.Contains("Gravy Sauce") &&                   //If all materials are collected
               ItemBox.Items.Contains("Pan") && ItemBox.Items.Contains("Knife") && ItemBox.Items.Contains("Cheese") /*&& StoveFixed == true*/)
            {
                //Mini game opens
                //Pearlly Molly and Emma to work on it
                ItemBox.Items.Remove("Potato");
                ItemBox.Items.Remove("Canola Oil");
                ItemBox.Items.Remove("Gravy Sauce");
                ItemBox.Items.Remove("Pan");
                ItemBox.Items.Remove("Knife");
                ItemBox.Items.Remove("Cheese");
                ItemBox.Items.Remove("Poutine Recipe");
                ItemBox.Items.Add("Poutine");

                Dialogue();
                DialogueBox.Text = "You have successfully cooked a poutine!";
            }
            else
            {
                Dialogue();
                DialogueBox.Text = "A cutting board. You don't have all the items yet.";
            }
        }

        private void picPotato()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 210;
            clickableItem.Height = 90;
            clickableItem.Location = new Point(50, 260);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += potato_click;
            Controls.Add(clickableItem);
        }
        private void potato_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Potato"))
            {
                Dialogue();
                DialogueBox.Text = "You obtained potato.";
                ItemBox.Items.Add("Potato");
            }
            else if(ItemBox.Items.Contains("Potato"))
            {
                Dialogue();
                DialogueBox.Text = "You don't need more potato.";
            }
        }

        private void picTomato()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 95;
            clickableItem.Height = 85;
            clickableItem.Location = new Point(125, 465);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += tomato_click;
            Controls.Add(clickableItem);
        }
        private void tomato_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Tomato"))
            {
                Dialogue();
                DialogueBox.Text = "A box of tomato. Mouse might like it.";
                ItemBox.Items.Add("Tomato");
            }
            else if (ItemBox.Items.Contains("Tomato"))
            {
                Dialogue();
                DialogueBox.Text = "You don't have enough room for more tomato.";
            }     
        }

        private void picOil()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 125;
            clickableItem.Height = 205;
            clickableItem.Location = new Point(915, 540);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += oil_click;
            Controls.Add(clickableItem);
        }
        private void oil_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Canola Oil"))
            {
                Dialogue();
                DialogueBox.Text = "You looked inside the barrel and find Canola oil.";
                ItemBox.Items.Add("Canola Oil");
            }
            else if(ItemBox.Items.Contains("Canola Oil"))
            {
                Dialogue();
                DialogueBox.Text = "The barrel is now empty.";
            }   
        }


        private void picMouse()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 60;
            clickableItem.Height = 50;
            clickableItem.Location = new Point(1250, 740);
           // clickableItem.Location = new Point(1050, 540);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += mouse_click;
            Controls.Add(clickableItem);
        }
        private void mouse_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Tomato"))
            {
                Dialogue();
                DialogueBox.Text = "The mouse looks very hungry. \n Maybe you could offer something.";
            }
            else if (ItemBox.Items.Contains("Tomato") && !ItemBox.Items.Contains("Frozen Cheese"))
            {
                Dialogue();
                DialogueBox.Text = "The mouse eats the tomato. To repay your kindness,\n he gives you frozen cheese.";
                ItemBox.Items.Remove("Tomato");
                ItemBox.Items.Add("Frozen Cheese");
            }    
        }


        private void picShowerHead()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 175;
            clickableItem.Height = 120;
            clickableItem.Location = new Point(630, 35);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += showerHead_click;
            Controls.Add(clickableItem);
        }
        private void picShowerHead2()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 175;
            clickableItem.Height = 120;
            clickableItem.Location = new Point(1100, 35);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += showerHead_click;
            Controls.Add(clickableItem);
        }
        private void showerHead_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "A shower Head.";
        }

        private void picHugeMirror()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 1130;
            clickableItem.Height = 235;
            clickableItem.Location = new Point(120, 200);
            clickableItem.BackColor = Color.Transparent ;
            clickableItem.Click += hugeMirror_click;
            Controls.Add(clickableItem);
        }
        private void hugeMirror_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "The mirror is covered with steam. Someone could write on it.";
        }
        private void picScribble()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 90;
            clickableItem.Height = 30;
            clickableItem.Location = new Point(1240, 400);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += scribble_click;
            clickableItem.BringToFront();
            Controls.Add(clickableItem);
        }
        private void scribble_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "23045";
        }

        private void picShampoo()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 80;
            clickableItem.Height = 100;
            clickableItem.Location = new Point(890, 380);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += shampoo_click;
            clickableItem.BringToFront();
            Controls.Add(clickableItem);
        }
        private void shampoo_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            Dialogue();
            DialogueBox.Text = "A box of shampoo and conditionners";
        }

        private void picBrokenWall ()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 125;
            clickableItem.Height = 250;
            clickableItem.Location = new Point(1120, 500);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += brokenWall_click;
            Controls.Add(clickableItem);
        }
        private void brokenWall_click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (!ItemBox.Items.Contains("Spoon"))
            {
                Dialogue();
                DialogueBox.Text = "The wall tile is broken. The inside walls looks soft \n almost you can dig through if you has an ITEM.";
            }
            else if (ItemBox.Items.Contains("Spoon"))
            {
                brokenWallClicked = true;
                Dialogue();
                ItemBox.Items.Remove("Poutine Recipe");
                ItemBox.Items.Remove("Spoon");
                DialogueBox.Text = "You started digging the wall with spoon, on and on \n until you started feeling a mere presence of light.";
            }
        }


        //Level 3 Clickable items

        private void picRiddler()
        {
            clickSprite2 = new PictureBox();
            clickSprite2.Width = 160;
            clickSprite2.Height = 570;
            clickSprite2.Location = new Point(220, 220);
            clickSprite2.BackColor = Color.Transparent;
            clickSprite2.Click += Riddler_click;
            Controls.Add(clickSprite2);
            clickSprite2.BringToFront();
        }
        private void Riddler_click(object sender, EventArgs e)
        {
            removeExtra();
            currentDialogueLine = new List<string>
            {
               "How did you even get here? I guess \n you dug through the wall in shower room.",
               "You are trying to escape? That’s insane,\nI love being locked in a prison more than anything."
            };
            currentDialogueIndex = 0;
            NpcTitle();
            npcTitle.Text = "Prisoner 01145";
            Dialogue();                 
            ShowNextLine();
        }
        private void picEmma2()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 130;
            clickableItem.Height = 490;
            clickableItem.Location = new Point(1050, 290);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Emma2_click;
            Controls.Add(clickableItem);
            clickableItem.BringToFront();
        }
        private void Emma2_click(object sender, EventArgs e)
        {
            removeExtra();

            if (currentBackground == backbuffer8)
            {
                if (Riddles == 0)
                {
                    Dialogue();
                    DialogueBox.Text = "We made it to the floor below. We're almost free.";
                }
                else if (Riddles == 1)                   //Hint to quesiton 2
                {
                    Dialogue();
                    DialogueBox.Text = "I guess this question might be related to numbers 1-10...";
                }
            }
            else if (currentBackground == backbuffer10)
            {
                currentDialogueLine = new List<string>
                {
                  "To get outside of the prison building, the only way is \n pulling down the fire alarm in staff room. \n As we evacuate we will escape.",
                };
                currentDialogueIndex = 0;
                NpcTitle();
                npcTitle.Text = "Emma Gibbs";
                Dialogue();
                ShowNextLine();
            }
            else if (currentBackground == backbuffer11) 
            {
                NpcTitle();
                npcTitle.Text = "Emma Gibbs";
                Dialogue();
                DialogueBox.Text = "Maybe click on the monitor screen before you go.";
            }
        }

        private void picBlankPaper()
        {
            clickSprite2 = new PictureBox();
            clickSprite2.Width = 30;
            clickSprite2.Height = 20;
            clickSprite2.Location = new Point(1225, 520);
            clickSprite2.BackColor = Color.Transparent;
            clickSprite2.Click += BlankPaper_click;
            Controls.Add(clickSprite2);
        }
        private void BlankPaper_click (object sender, EventArgs e)
        {
            removeExtra();

            if (Riddles == 2)
            {
                ItemBox.Items.Add("Blank Paper");
                Dialogue();
                DialogueBox.Text = "You can see slight letters written on paper but it's blurry. \n What can you do to see better? \n *Obatained a blank paper*";
            }
        }
        private void picLight()
        { 
            clickSprite2 = new PictureBox();
            clickSprite2.Width = 35;
            clickSprite2.Height = 60;
            clickSprite2.Location = new Point(1010, 320);
            clickSprite2.BackColor = Color.Transparent;
            clickSprite2.Click += Light_click;
            Controls.Add(clickSprite2);
        }
        private void Light_click(object sender, EventArgs e)
        {
            removeExtra();

            if (Riddles == 2 && ItemBox.Items.Contains("Blank Paper"))         //Hint for 3rd Question
            {
                Dialogue();
                DialogueBox.Text = "As you shine light to the blank paper blurry letter starts appearing. \n Hint: Pronoun for female + r + another word for tree ";
            }
        }

        private void picSingleBed()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 850;
            clickableItem.Height = 230;
            clickableItem.Location = new Point(200, 480);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += SingleBed_click;
            Controls.Add(clickableItem);
        }
        private void SingleBed_click(object sender, EventArgs e)
        {
            removeExtra();

            Dialogue();
            DialogueBox.Text = "A comfortable looking bed.";
        }

        private void picHidddenSpot()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 100;
            clickableItem.Height = 30;
            clickableItem.Location = new Point(670, 450);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += HidddenSpot_click;
            Controls.Add(clickableItem);
        }
        private void HidddenSpot_click(object sender, EventArgs e)
        {
            removeExtra();
            hiddenSpotClicked = true;                   //To create an if statment in ShowNextLine() (if hiddenspot is clicked, then..)
            if (Riddles == 0)
            {
                currentDialogueLine = new List<string>
                {
               "That’s an escape hole I made. It is connected \n to another room. You want to use it?",
               "Sorry but you can’t.\n Unless you can solve my riddles\n Then I can consider it."
                };
                currentDialogueIndex = 0;
                NpcTitle();
                npcTitle.Text = "Prisoner 01145";
                Dialogue();
                ShowNextLine();
            }
        }
        private void picBin()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 230;
            clickableItem.Height = 290;
            clickableItem.Location = new Point(1170, 545);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Bin_click;
            Controls.Add(clickableItem);
        }
        private void Bin_click(object sender, EventArgs e)
        {
            removeExtra();

            Dialogue();
            DialogueBox.Text = "A garbage bin.";
            
        }

        //Escape Hole in backbuffer9

        private void picEscape()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 180;
            clickableItem.Height = 190;
            clickableItem.Location = new Point(640, 480);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Escape_click;
            Controls.Add(clickableItem);
        }
        private void Escape_click(object sender, EventArgs e)
        {
            removeExtra();
            currentBackground = backbuffer10;
            Invalidate();
            background_Change();
        }

        //CCTV ROOM CLICKABLE ITEM

        private void picPowerBox()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 170;
            clickableItem.Height = 220;
            clickableItem.Location = new Point(1170, 100);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += PowerBox_click;
            Controls.Add(clickableItem);
        }
        private void PowerBox_click(object sender, EventArgs e)
        {
            removeExtra();

            Dialogue();
            DialogueBox.Text = "You have powered on the electricity";
            ElectricityOn = true;
        }

        private void picStep()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 220;
            clickableItem.Height = 120;
            clickableItem.Location = new Point(520, 680);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Step_click;
            Controls.Add(clickableItem);
        }
        private void Step_click(object sender, EventArgs e)
        {
            removeExtra();
            
            if(!ItemBox.Items.Contains("Step"))
            {
                Dialogue();
                DialogueBox.Text = "You have obtained a step";
                ItemBox.Items.Add("Step");
            }
            else
            {
                Dialogue();
                DialogueBox.Text = "You already posses a step";
            }
        }

        private void picWire()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 70;
            clickableItem.Height = 50;
            clickableItem.Location = new Point(260, 420);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Wire_click;
            Controls.Add(clickableItem);
        }
        private void Wire_click(object sender, EventArgs e)
        {
            removeExtra();

            UntangleWire = true;
            Dialogue();
            DialogueBox.Text = "You have untangled the wire knots";
        }

        private void picMonitor()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 366;
            clickableItem.Height = 220;
            clickableItem.Location = new Point(350, 190);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += Monitor_click;
            Controls.Add(clickableItem);
        }
        private void Monitor_click(object sender, EventArgs e)
        {
            removeExtra();

            if(currentBackground == backbuffer10)                         //When monitor screen are not powered on yet
            {
                Dialogue();
                DialogueBox.Text = "The monitor screen is off. You should find a way to power on this.";
            }
            else if (currentBackground == backbuffer11)                   //When monitor screen is ON
            {
                Dialogue();
                DialogueBox.Text = "Oh no, Officer French is inside staff room. Before entering the room \n you'll need to change your prisoner cloth to something else.";
                NpcTitle();
                npcTitle.Text = "Emma Gibbs";
         
            }
        }
        private void picWhiteCabinet()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 366;
            clickableItem.Height = 220;
            clickableItem.Location = new Point(350, 190);
            clickableItem.BackColor = Color.FromArgb(128, Color.Transparent);
            clickableItem.Click += WhiteCabinet_click;
            Controls.Add(clickableItem);
        }
        private void WhiteCabinet_click(object sender, EventArgs e)
        {
            removeExtra();

            if (!ItemBox.Items.Contains("Storage Box Key"))                         //When monitor screen are not powered on yet
            {
                Dialogue();
                DialogueBox.Text = "The monitor screen is off. You should find a way to power on this.";
            }
        }


        private void picPowerButton()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 40;
            clickableItem.Height = 50;
            clickableItem.Location = new Point(320, 485);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += PowerButton_click;
            Controls.Add(clickableItem);
        }
        private void PowerButton_click(object sender, EventArgs e)
        {
            removeExtra();

            if(UntangleWire == true && ElectricityOn == true && ItemBox.Items.Contains("Step"))          //When all the conditions are valid background changes to monitor screen ON
            {
                currentBackground = backbuffer11;
                Invalidate();
                background_Change();
            }
            else                                                                                         //Else nothing happens
            {
                Dialogue();
                DialogueBox.Text = "No changes are seen. Maybe something must be activated.";
            }

        }

        //CLOTHING ROOM CLICKABLE ITEM

        private void picstorageBox()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 260;
            clickableItem.Height = 150;
            clickableItem.Location = new Point(75, 640);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += storageBox_click;
            Controls.Add(clickableItem);
        }
        private void storageBox_click(object sender, EventArgs e)
        {
            removeExtra();

            if (ItemBox.Items.Contains("Storage Box Key") && !ItemBox.Items.Contains("Hammer"))       //If user has the key but without a hammer (prevent obtaining item again)
            {
                Dialogue();
                DialogueBox.Text = "You have opended the storage box and obtained a hammer.";
                ItemBox.Items.Add("Hammer");
            }
            else if (!ItemBox.Items.Contains("Storage Box Key"))                                     //If user doesn't have the key
            {
                Dialogue();
                DialogueBox.Text = "Storage Box is locked";
            }
        }

        private void picWardenCloth()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 60;
            clickableItem.Height = 100;
            clickableItem.Location = new Point(590,110);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += WardenCloth_click;
            Controls.Add(clickableItem);
        }
        private void WardenCloth_click(object sender, EventArgs e)
        {
            removeExtra();

            if(ItemBox.Items.Contains("Step"))
            {
                ItemBox.Items.Remove("Step");
                Dialogue();
                DialogueBox.Text = "You have changed your outfit to warden cloth.";
                WardenCloth = true;
            }
            else if (!ItemBox.Items.Contains("Step"))
            {
                Dialogue();
                DialogueBox.Text = "The cloth hung too high to reach.";
            }
        }

        //Staff room

        private void picAC()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 600;
            clickableItem.Height = 120;
            clickableItem.Location = new Point(445, 30);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += AC_Click;
            Controls.Add(clickableItem);
        }

        private void AC_Click(object sender, EventArgs e)
        {
            removeExtra();
            Dialogue();
            DialogueBox.Text = "The AC is turned on.";
        }


        private void picFireAlarm()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 130;
            clickableItem.Height = 150;
            clickableItem.Location = new Point(1245, 175);
            clickableItem.BackColor = Color.FromArgb(128, Color.Transparent);
            clickableItem.Click += FireAlarm_Click;
            Controls.Add(clickableItem);
        }

        private void FireAlarm_Click(object sender, EventArgs e)
        {
            removeExtra();

        }

        private void picStaffCup()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 50;
            clickableItem.Height = 70;
            clickableItem.Location = new Point(760, 565);
            clickableItem.BackColor = Color.Transparent;
            clickableItem.Click += StaffCup_Click;
            Controls.Add(clickableItem);
        }

        private void StaffCup_Click(object sender, EventArgs e)
        {
            removeExtra();

            if (!ItemBox.Items.Contains("Cup"))
            {
                ItemBox.Items.Add("Cup");
            }
        }

        private void picWatch()
        {
            clickableItem = new PictureBox();
            clickableItem.Width = 170;
            clickableItem.Height = 20;
            clickableItem.Location = new Point(780, 625);
            clickableItem.BackColor = Color.FromArgb(128, Color.Transparent);
            clickableItem.Click += picWatch_Click;
            Controls.Add(clickableItem);
        }

        private void picWatch_Click(object sender, EventArgs e)
        {
            removeExtra();
            if(!ItemBox.Items.Contains("Watch"))
            {
                ItemBox.Items.Add("Watch");
            }
        }


        private void removeExtra()     //Some controls needs to be removed (ex Dialogue) before opening another one to prevent error.
        {
            Controls.Remove(RiddleBox);
            Controls.Remove(LockBox);
            Controls.Remove(DialogueBox);
            Controls.Remove(npcTitle);
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
                g.DrawImage(sprite2, rect2);
            }
            if(background == backbuffer8)
            {
                g.DrawImage(sprite3, rect3);
                g.DrawImage(sprite2, rect2);
            }
            if (background == backbuffer9)
            {
                g.DrawImage(sprite3, rect3);
                g.DrawImage(sprite2, rect2);
            }
            if (background == backbuffer10)
            {
                g.DrawImage(sprite2, rect2);
            }
            if (background == backbuffer11)
            {
                g.DrawImage(sprite2, rect2);
            }
            if (background == backbuffer13)
            {
                g.DrawImage(sprite, rectDest);
            }
        }   
        private void picArrowL_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);                         //For better UX, if user move between rooms without removing dialogue yet it automatically does
            Controls.Remove(LockBox);                          
            Controls.Remove(RiddleBox);

            if (currentBackground == backbuffer1)                          //For level 1, when cell door is still locked
            {
                currentBackground = backbuffer2;
                background_Change();
            } 
            else if (currentBackground == backbuffer4)
            {
                currentBackground = backbuffer5;                  //Go to Kitchen
                background_Change();
            }
            else if (currentBackground == backbuffer5)
            {
                currentBackground = backbuffer7;                  //Go to Storage
                background_Change();
            }
            else if (currentBackground == backbuffer6)
            {
                currentBackground = backbuffer4;                  //Return Back to cell Hallway from Shower Booth
                background_Change();
            }
            else if (currentBackground == backbuffer12)
            {
                currentBackground = backbuffer11;                  //Return to cctv Room from clothing room
                background_Change();
            }
            else if (currentBackground == backbuffer13)
            {
                currentBackground = backbuffer12;                  //Return to Clothing room from staff room
                background_Change();
            }

        }

        private void picArrowR_Click(object sender, EventArgs e)
        {
            Controls.Remove(DialogueBox);
            if (currentBackground == backbuffer2)
            {
                currentBackground = backbuffer1;              //Return to cell Door from BedRoom
                background_Change();
            }
            else if(currentBackground == backbuffer4)
            {
                currentBackground = backbuffer6;              //Return to Cell Hallway
                background_Change();
            }
            else if (currentBackground == backbuffer5)
            {
                currentBackground = backbuffer4;              //Changes to Shower Room
                background_Change();
            }
            else if (currentBackground == backbuffer7)
            {
                currentBackground = backbuffer5;              //Return to Kitchen
                background_Change();
            }
            else if (currentBackground == backbuffer11)
            {
                currentBackground = backbuffer12;              //Go to Clothing room
                background_Change();
            }
            else if (currentBackground == backbuffer12)         //Go to Staff room
            {
                if(WardenCloth == true)                         //IF user have changed their outfit to warden outfit they can enter staff room
                {
                    currentBackground = backbuffer13;              
                    background_Change();
                }
                else                                          //Or else they are denied to proceed
                {
                    Dialogue();
                    DialogueBox.Text = "You can't enter the staff room with prisoner's cloth";
                }
               
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
                Room_Name();
                RoomName.Text = "Cell Door";
                wardenClick();
                picCellDoor();
                picExitPortal();
            }
            else if (currentBackground == backbuffer2)        //Items that goes along with background2
            {
                Room_Name();
                RoomName.Text = "Bedroom";
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
                Room_Name();
                RoomName.Text = "Cell Corridor";
                picElectricLock();
                picVent();
                picEmmaLocked();
            }
            else if (currentBackground == backbuffer4)
            {
                Room_Name();
                RoomName.Text = "Cell Corridor";
                picEmma();
            }
            else if (currentBackground == backbuffer5)
            {
                Room_Name();
                RoomName.Text = "Kitchen";
                picOven();
                picKnife();
                picPan();
                picCupBoard();
                picPlate();
                picStove();
                picCuttingBoard();
            }
            else if (currentBackground == backbuffer6)
            {
                Room_Name();
                RoomName.Text = "Shower Room";
                picHugeMirror();
                picScribble();
                picShampoo();
                picBrokenWall();
                picShowerHead();
                picShowerHead2();

            }
            else if (currentBackground == backbuffer7)
            {
                Room_Name();
                RoomName.Text = "Storage";
                picPotato();
                picTomato();
                picOil();
                picMouse();
            }
            else if (currentBackground == backbuffer8)
            {
                Room_Name();
                RoomName.Text = "Solitary Confinement";
                picRiddler();
                picSingleBed();
                picHidddenSpot();
                picBlankPaper();
                picBin();
                picLight();
                picEmma2();
            }
            else if (currentBackground == backbuffer9)
            {
                Room_Name();
                RoomName.Text = "Solitary Confinement";
                picEscape();
            }
            else if (currentBackground == backbuffer10)
            {
                Room_Name();
                RoomName.Text = "CCTV Room";
                picEmma2();
                picStep();
                picPowerBox();
                picWire();
                picPowerButton();
                picMonitor();
            }
            else if (currentBackground == backbuffer11)
            {
                Room_Name();
                RoomName.Text = "CCTV Room";
                picEmma2();
                picMonitor();
            }
            else if(currentBackground == backbuffer12)
            {
                Room_Name();
                RoomName.Text = "Cloth Room";
                picstorageBox();
                picWardenCloth();
            }
            else if (currentBackground == backbuffer13)
            {
                Room_Name();
                RoomName.Text = "Staff Room";
                picAC();
                picFireAlarm();
                picStaffCup();
                picWatch();
            }

            Invalidate();                //Force the form to RE-DRAW
        }
    }
}
//Goals
//Fix problem where you can click kitech item when riddle box is open
//Program closing
//Remove arrowR for backbuffer 1 but make it appear as it goes to backbuffer2
//Character idle (If time allows)


