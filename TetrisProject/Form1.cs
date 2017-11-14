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
        private bool ResetAble;

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

            myBoard = new Board(panelBoard);

            currentPiece = Piece.GenerateRandomPieceOnTop();

            nextPiece = Piece.GenerateRandomPieceOnTop();

            ResetAble = false;

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
        private void SetSpeed(int Level,bool downKey = false)
        {
            if (downKey)
            {
                timer1.Interval = 1;
                return;
            }
            timer1.Interval = (int)(GameConstants.baseInterval * Math.Pow(GameConstants.speedIncrease, (double)Level))/5;
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)   //hanlde the reprinting of the board panel
        {
            currentPiece.DisplayBoard(panelBoard);
            myBoard.DisplayBoard();
            //synchronize the redisplay content with active dispaly
        }

        private void nextBlockPanel_Paint(object sender, PaintEventArgs e)   //handel the reprinting of the nextblock panel
        {
            //synchronize the redisplay content with active dispaly
            nextPiece.DisplayNext(nextBlockPanel);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.Falling(myBoard);

                if (status == 1)
                {
                    myBoard.AddPiece(currentPiece);
                    myBoard.CheckAndClearLines();
                    Board.ClearDisplayBoard(panelBoard);
                    myBoard.DisplayBoard();
                    if (myBoard.CheckLoss())
                    {
                        test.Visible = true;
                        ResetAble = true;
                        return;
                    }
                    currentPiece = nextPiece;
                    nextPiece = Piece.GenerateRandomPieceOnTop();
                    currentPiece.DisplayBoard(panelBoard);
                    Piece.DisappearNext(nextBlockPanel);
                    nextPiece.DisplayNext(nextBlockPanel);
                }
                else
                {
                    currentPiece.DisplayBoard(panelBoard);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveLeft(myBoard);

                currentPiece.DisplayBoard(panelBoard);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveRight(myBoard);

                currentPiece.DisplayBoard(panelBoard);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.CheckRotateClockwise(myBoard);

                currentPiece.DisplayBoard(panelBoard);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.CheckRotateCounterClockwise(myBoard);

                currentPiece.DisplayBoard(panelBoard);
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            if(ResetAble)
            {
                myBoard = new Board(panelBoard);
                Board.ClearDisplayBoard(panelBoard);
                myBoard.DisplayBoard();
                currentPiece = Piece.GenerateRandomPieceOnTop();
                nextPiece = Piece.GenerateRandomPieceOnTop();
                Piece.DisappearNext(nextBlockPanel);
                nextPiece.DisplayNext(nextBlockPanel);
                ResetAble = false;
                test.Visible = false;
            }
        }

        private void TetrisFrom_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.P:
                    Pause(this);
                    break;
                case Keys.Down:
                    SetSpeed(0, true);
                    break;
                case Keys.Up:
                    
                    break;
                case Keys.Left:
                    if (!ResetAble)
                    {
                        currentPiece.DisappearBoard(panelBoard);

                        int status = currentPiece.MoveLeft(myBoard);

                        currentPiece.DisplayBoard(panelBoard);
                    }
                    break;
                case Keys.Right:
                    if (!ResetAble)
                    {
                        currentPiece.DisappearBoard(panelBoard);

                        int status = currentPiece.MoveRight(myBoard);

                        currentPiece.DisplayBoard(panelBoard);
                    }
                    break;
                default:
                    break;
            }

        }



        private void MoveDown()
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.Falling(myBoard);

                if (status == 1)
                {
                    myBoard.AddPiece(currentPiece);
                    myBoard.CheckAndClearLines();
                    Board.ClearDisplayBoard(panelBoard);
                    myBoard.DisplayBoard();
                    if (myBoard.CheckLoss())
                    {
                        test.Visible = true;
                        ResetAble = true;
                        return;
                    }
                    currentPiece = nextPiece;
                    nextPiece = Piece.GenerateRandomPieceOnTop();
                    currentPiece.DisplayBoard(panelBoard);
                    Piece.DisappearNext(nextBlockPanel);
                    nextPiece.DisplayNext(nextBlockPanel);
                }
                else
                {
                    currentPiece.DisplayBoard(panelBoard);
                }
            }
        }

        private void MoveLeft()
        {
            if (!ResetAble)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveLeft(myBoard);

                currentPiece.DisplayBoard(panelBoard);
            }
        }



        private void Update()
        {

        }



        int step = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            step = (step + 1) % 5;
            Update();
            if(step == 0)MoveDown();
        }

        private void TetrisFrom_KeyUp(object sender, KeyEventArgs e)
        {
            SetSpeed(currentLevel);
        }
    }
    

}
