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
        private Piece currentPiece;               //used to point to the current piece

        private Piece nextPiece;                  //used to point to the next piece

        private Board myBoard;                    //used to do the calculations and provide abstaction with the board class

        private enum PlayStatus         //The Status of the current game
        {
            NotPlay,                    //The game has not been played.
            PlayNotPause,               //The game is currently playing
            PlayPause                   //The game is paused
        }

        private PlayStatus currentGameStatus;

        private static int currentLevel = 1;        //The current level of this game

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

            currentPiece = new LongPiece(Piece.RotationStateClockwise.CW0, 5, 4);

        }
        private void TetrisFrom_Load(object sender, EventArgs e)
        {

        }

        private void PlayPauseButton_Click(object sender, EventArgs e)
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

        private void panelBoard_Paint(object sender, PaintEventArgs e)   //hanlde the reprinting of the board panel
        {
            //synchronize the redisplay content with active dispaly
            currentPiece.DisplayBoard(panelBoard);
            BlockGraphics.DisplayBlock(2, panelBoard, 2, 1);
        }

        private void nextBlockPanel_Paint(object sender, PaintEventArgs e)   //handel the reprinting of the nextblock panel
        {
            //synchronize the redisplay content with active dispaly
            BlockGraphics.DisplayBlock(2, nextBlockPanel, 3, 3);
        }
    }
}
