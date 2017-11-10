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

        private enum PlayStatus         //The Status of the current game
        {
            NotPlay,                    //The game has not been played.
            PlayNotPause,               //The game is currently playing
            PlayPause                   //The game is paused
        }

        private PlayStatus currentGameStatus;

        private static int currentLevel = 1;

        public TetrisFrom()
        {
            InitializeComponent();

            //initalize the board item
            
            currentGameStatus = PlayStatus.NotPlay;      //initialize the state of the game to not playing

            //Set the position and Size of Components

            this.ClientSize = new System.Drawing.Size(VisualConstants.WindowSizeX, VisualConstants.WindowsSizeY);  //Set the size of the window

            this.panelBoard.Location = new System.Drawing.Point(VisualConstants.leftMargin, VisualConstants.upperMargin + VisualConstants.MenuSzie ); //Set the location of the board

            this.panelBoard.Size = new System.Drawing.Size(VisualConstants.boardSizeX, VisualConstants.boardSizeY); //Set the size of the board

            this.nextBlockLabel.Location = new System.Drawing.Point(VisualConstants.LabelX, VisualConstants.MenuSzie + VisualConstants.upperMargin);  //set the location of the next block label

            this.nextBlockLabel.Size = new System.Drawing.Size(VisualConstants.LabelWidth, VisualConstants.LabelHeight); //Set the size of the next block label

            this.BackColor = VisualConstants.formColor;    //Set the background color

            this.panelBoard.BackColor = VisualConstants.boardBackground;    //set the background of the panel



        }
        private void TetrisFrom_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           if(currentGameStatus == PlayStatus.NotPlay)
            {
                //generate event / call methods from timer class for timer for starting the game
                Play(this);
            }
           else if (currentGameStatus == PlayStatus.PlayNotPause)
            {
                //generate event / call methods from timer class for timer to pause the game
                Pause(this);
            }
           else if(currentGameStatus == PlayStatus.PlayPause)
            {
                //generate event / call methods from timer class for timer to resume the game
                Resume(this);
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
BlockGraphics myBlock = new BlockGraphics(Color.Red, panelBoard, 1, 1);
myBlock.DisplayBlock();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Play(TetrisFrom odj)
        {

            currentGameStatus = PlayStatus.PlayNotPause;
            playPauseButton.Text = "Pause";
            timer1.Enabled = true;
            SetSpeed(currentLevel);
            timer1.Start();

        } 
        private void Pause(TetrisFrom odj)
        {
            timer1.Stop();
            currentGameStatus = PlayStatus.PlayPause;
            playPauseButton.Text = "Resume";
        }
        private void Resume(TetrisFrom odj)
        {
            timer1.Start();
            currentGameStatus = PlayStatus.PlayNotPause;
            playPauseButton.Text = "Pause";
        }
        private void SetSpeed(int Level)
        {
            timer1.Interval = (int)(GameConstants.baseInterval * Math.Pow(GameConstants.speedIncrease, (double)Level));
        }
    }
}
