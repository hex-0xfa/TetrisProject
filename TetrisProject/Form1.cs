using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    public partial class TetrisFrom : Form
    {
        private int i = 1;

        private enum PlayStatus         //The Status of the current game
        {
            NotPlay,                    //The game has not been played.
            PlayNotPause,               //The 
            PlayPause
        }

        private PlayStatus currentGameStatus;

        public TetrisFrom()
        {
            InitializeComponent();
            currentGameStatus = PlayStatus.NotPlay;
            this.ClientSize = new System.Drawing.Size(VisualConstants.WindowSizeX, VisualConstants.WindowsSizeY);  //Set the size of the window

            this.panelBoard.Location = new System.Drawing.Point(VisualConstants.leftMargin, VisualConstants.upperMargin + VisualConstants.MenuSzie ); //Set the location of the board

            this.panelBoard.Size = new System.Drawing.Size(VisualConstants.boardSizeX, VisualConstants.boardSizeY); //Set the size of the board
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = 2;

        }

        private void TetrisFrom_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           if(currentGameStatus == PlayStatus.NotPlay)
            {
                //generate event / call methods from timer class for timer for starting the game
                currentGameStatus = PlayStatus.PlayNotPause;
                playPauseButton.Text = "Pause";
 
            }
           else if (currentGameStatus == PlayStatus.PlayNotPause)
            {
                //generate event / call methods from timer class for timer to pause the game
                currentGameStatus = PlayStatus.PlayPause;
                playPauseButton.Text = "Resume";
            }
           else if(currentGameStatus == PlayStatus.PlayPause)
            {
                //generate event / call methods from timer class for timer to resume the game
                currentGameStatus = PlayStatus.PlayNotPause;
                playPauseButton.Text = "Pause";
            }
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            playPauseButton.ForeColor = Color.Green;

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            playPauseButton.ForeColor = Color.Red;
        }

        private void board_Paint(object sender, PaintEventArgs e)
        {
            //display the content of the board.
            //display the piece if any.
            Block myBlock = new Block(Color.Red, panelBoard, i, 1);
            myBlock.DisplayBlock();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
