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
    //Emma Gibbs, Pearl
    //May 14th -  2024 
    //This is the minigames form for when the player gets to a point that requiers a minigame to get to the next portion of the game
    public partial class Form4 : Form
    {
        //Variables:
        //The minigames (if they're in use or not):
        bool brokenFaucetMG = false;
        bool stdCabinetMG = false;
        bool kitchenMazeMG = false;
        bool makeFoodMG = false;
        bool digMG = false;
        //Broken faucet game variables:
        int piecesFound;
        bool pieceBed = false; //fauset piece under pillow
        bool pieceCabinet = false; //fauset piece in Cabenet
        bool pieceTowels = false; //fauset piece hidden inbetween towels


        //Level 1 minigames
        private void BrokenFaucet() //Broken Faucet Minigame
        {
            //Comments on this mini game
            {
                //The player will click the fauset to find that its broken, so they cant spill water on the warden yet
                //They will then have to find broken pieces of the fauset by clicking on numberous objects in the cell.
                //Pieces found counter
            }
            //Code:
            brokenFaucetMG = true;

        }
        private void SpotTheDifferenceCabinet() //Part of BrokenFaucet //Spot the difference minigame when the player clicks cabinet
        {
            //Comments on this mini game
            {
                //This is a spot the difference minigame
                //When the player clicks the Cabinet it will lead to this minigame
                //Completing this minigame will grant the player a piece of the fauset
            }
            //Code:
            stdCabinetMG = true;
            pieceCabinet = true;//put this at the end of code in this procedure
        }
        //Level 2 minigames:
        private void KitchenMaze() //Maze to get to the kitchen minigame
        {
            //Comments on this mini game
            {
                //This is a mazegame
                //have walls the player cant go over
                //
            }
            //Code:
            kitchenMazeMG = true;
        }
        private void MakeFood() //Making Food for Emma minigame
        {
            //Comments on this mini game
            {

            }
            //Code:
            makeFoodMG = true;
        }
        //Level 3 minigame:
        private void Dig() //Digging to get to next level minigame (going to crazy guy's cell)
        {
            //Comments on this mini game
            {

            }
            //Code:
            digMG = true;
        }

        private void btnHint_Click(object sender, EventArgs e) //Hint Button
        {
            //Comments for this button
            {
                //This is the hint button for when players need help
                //Button will give different hints depending on what mini game the user is currently playing
            }
            //Code:
            if (brokenFaucetMG == true)
            {
                if (stdCabinetMG == true)
                {
                    MessageBox.Show("Find the differences to get the faucet piece from the cabenet");
                }
                else
                {
                    MessageBox.Show("Click the diffrent piece of furniture to find the faucet pieces");
                }
            }
            else if (kitchenMazeMG == true)
            {
                MessageBox.Show("Use the arrow keys to escape the maze");
            }
            else if (makeFoodMG == true)
            {
                MessageBox.Show("Follow Emma's instructions to create he perfect poutine!");
            }
            else if (digMG == true)
            {
                MessageBox.Show("Just keep clicking...");
            }
        }
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
