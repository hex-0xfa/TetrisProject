using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TetrisProject
{
    public partial class TetrisForm : Form
    {
        DateTime startTime;                      //used to pause the timer

        bool speedIncreased;

        int restTime;                            //used to pasue the timer

        private Piece currentPiece;               //used to point to the current piece

        private Piece nextPiece;                  //used to point to the next piece

        private Board myBoard;                    //used to do the calculations and provide abstaction with the board class

        private int currentFallingSpeed;          //keep the current falling

        private int increasedSpeed;               //The speed after increase

        private enum PlayStatus          //The Status of the current game
        {
            Inactive,                    //The game has not been played.
            Game,                        //The game is currently playing
            Pause                        //The game is paused
        }

        private PlayStatus currentGameStatus;   //The current game state

        private int currentLevel;        //The current level of this game

        private int linesCleared;        //The lines cleared

        private int scores;              //The scores earned

        public TetrisForm()              //constructor
        {
            InitializeComponent();

            currentPiece = null;

            nextPiece = null;

            myBoard = null;

            //initalize the board item

            ChangeGameStatus(PlayStatus.Inactive);      //initialize the state of the game to not playing

            //Set the position and Size of Components

            this.ClientSize = new System.Drawing.Size(VisualConstants.WindowSizeX, VisualConstants.WindowsSizeY);  //Set the size of the window

            this.panelBoard.Location = new System.Drawing.Point(VisualConstants.leftMargin, VisualConstants.upperMargin + VisualConstants.MenuSzie); //Set the location of the board

            this.panelBoard.Size = new System.Drawing.Size(VisualConstants.boardSizeX, VisualConstants.boardSizeY); //Set the size of the board

            this.nextBlockLabel.Location = new System.Drawing.Point(VisualConstants.LabelX, VisualConstants.MenuSzie + VisualConstants.upperMargin);  //set the location of the next block label

            this.nextBlockLabel.Size = new System.Drawing.Size(VisualConstants.LabelWidth, VisualConstants.LabelHeight); //Set the size of the next block label

            this.BackColor = VisualConstants.formColor;    //Set the background color

            this.panelBoard.BackColor = VisualConstants.boardBackground;    //set the background of the panel

            // above not perfect for setting the sizes

            ResetScore();

            ResetLines();

            ResetLevelInactive();

            test.Visible = false;

            speedIncreased = false;
        }

        private void PlayPauseButton_Click(object sender, EventArgs e)   //what happens when the play pause button is clikcked
        {
            if (currentGameStatus == PlayStatus.Inactive)
            {
                StartNewGame();
            }
            else if (currentGameStatus == PlayStatus.Game)
            {
                PauseGame();
            }
            else if (currentGameStatus == PlayStatus.Pause)
            {
                ResumeGame();
            }
        }

        private void ChangeGameStatus(PlayStatus myPlayStatus)   //changing the state of this game
        {
            if(myPlayStatus == PlayStatus.Inactive)
            {
                currentGameStatus = PlayStatus.Inactive;
                playPauseButton.Text = "Play";
                TheTimer.Enabled = false;
                speedIncreased = false;
            }
            else if(myPlayStatus == PlayStatus.Game)
            {
                currentGameStatus = PlayStatus.Game;
                playPauseButton.Text = "Pause";
                TheTimer.Enabled = true;
                startTime = DateTime.Now;
                speedIncreased = false;
            }
            else
            {
                currentGameStatus = PlayStatus.Pause;
                playPauseButton.Text = "Resume";
                TheTimer.Enabled = false;
            }
        }

        public void QuitProgram()  //All three
        {

        }      

        public void ExitGame()  // Game  Pause
        {
            if ((currentGameStatus == PlayStatus.Game) || (currentGameStatus == PlayStatus.Pause))
            {
                ChangeGameStatus(PlayStatus.Inactive);
            }
        }

        public void StartNewGame()  //Inactive Game Pause
        {
            Board.ClearDisplayBoard(panelBoard);
            myBoard = new Board(panelBoard);
            myBoard.DisplayBoard();
            currentPiece = Piece.GenerateRandomPieceOnTop();
            currentPiece.DisplayBoard(panelBoard);
            nextPiece = Piece.GenerateRandomPieceOnTop();
            Piece.DisappearNext(nextBlockPanel);
            nextPiece.DisplayNext(nextBlockPanel);
            test.Visible = false;
            ResetScore();
            ResetLines();
            RestartLevel();
            currentFallingSpeed = (int)GameConstants.baseInterval;
            increasedSpeed = (int)GameConstants.maximumFastFallingSpeed;
            TheTimer.Interval = currentFallingSpeed;
            ChangeGameStatus(PlayStatus.Game);
        }

        public void LostGame()  //game
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                test.Visible = true;   //tell the user they have lost

                ChangeGameStatus(PlayStatus.Inactive);
            }
        }

        public void PauseGame()    //Game
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                restTime = (DateTime.Now.Subtract(startTime)).Milliseconds;
                ChangeGameStatus(PlayStatus.Pause);
            }
        }

        public void ResumeGame()   //Pasue
        {
            if (currentGameStatus == PlayStatus.Pause)
            {
                TheTimer.Interval = TheTimer.Interval - restTime;
                ChangeGameStatus(PlayStatus.Game);
            }
        }

        public void LoadGame()     //Inactive
        {

        }

        public void SaveGame()     //Pause
        {

        }

        public void AddLinesCleared(int lines)
        {
            if ((lines < 1) || (lines > GameConstants.pieceGridSizeY))
            {
                return;
            }
            AddScores(lines);
            AddLines(lines);
            if (linesCleared >= currentLevel * GameConstants.nextLevelLines)
            {
                AdvanceLevel();
            }
        }

        private void AddScores(int lines)
        {
            scores = scores + currentLevel * GameConstants.ModifiedBasePoints(lines);
            UpdateDisplayScore();
        }

        private void AdvanceLevel()
        {
            if ((currentGameStatus == PlayStatus.Game) || (currentGameStatus == PlayStatus.Pause))
            {
                currentLevel = currentLevel + 1;
                UpdateDisplayLevel();
                currentFallingSpeed = (int)(currentFallingSpeed * GameConstants.speedIncrease);
            }
        }

        private void AddLines(int lines)
        {
            linesCleared = linesCleared + lines;
            UpdateDisplayLines();
        }

        //Scores Display and reset Related

        private void UpdateDisplayScore()
        {
            socreNumber.Text = scores.ToString();
        }

        private void ResetScore()
        {
            scores = 0;
            socreNumber.Text = scores.ToString();
        }

        private void UpdateDisplayLines()
        {
            linesNumber.Text = linesCleared.ToString();
        }

        private void ResetLines()
        {
            linesCleared = 0;
            linesNumber.Text = linesCleared.ToString();
        }

        private void UpdateDisplayLevel()
        {
            levelNumber.Text = currentLevel.ToString();
        }

        private void RestartLevel()
        {
            currentLevel = 1;
            levelNumber.Text = currentLevel.ToString();
        }

        private void ResetLevelInactive()
        {
            currentLevel = 0;
            levelNumber.Text = currentLevel.ToString();
        }



        //display related functions

        private void label1_MouseHover(object sender, EventArgs e)
        {
            playPauseButton.ForeColor = Color.Green;

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            playPauseButton.ForeColor = Color.Red;
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)   //hanlde the reprinting of the board panel
        {
            if (currentPiece != null)
            {
                currentPiece.DisplayBoard(panelBoard);
            }
            if(myBoard != null)
            {
                myBoard.DisplayBoard();
            }
            //synchronize the redisplay content with active dispaly
        }

        private void nextBlockPanel_Paint(object sender, PaintEventArgs e)   //handel the reprinting of the nextblock panel
        {
            //synchronize the redisplay content with active dispaly
            if(nextPiece != null)
            {
                nextPiece.DisplayNext(nextBlockPanel);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(speedIncreased == true)
            {
                TheTimer.Interval = increasedSpeed;
            }
            else
            {
                TheTimer.Interval = currentFallingSpeed;
            }
            restTime = 0;
            startTime = DateTime.Now;
            Falling();
        }

        private void MoveLeft()
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveLeft(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void MoveRight()
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveRight(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void ClockwiseRotating()
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.CheckRotateClockwise(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void CounterClockwiseRotating()
        {
                if (currentGameStatus == PlayStatus.Game)
                {
                    currentPiece.DisappearBoard(panelBoard);

                    int status = currentPiece.CheckRotateCounterClockwise(myBoard);

                    currentPiece.DisplayBoard(panelBoard);

                    myBoard.DisplayBoard();
                }
        }

        private void Falling()
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.Falling(myBoard);

                if (status == 1)            //The piece is fallen on the board
                {
                    myBoard.AddPiece(currentPiece);

                    int lines = myBoard.CheckAndClearLines();
                    AddLinesCleared(lines);

                    Board.ClearDisplayBoard(panelBoard);
                    myBoard.DisplayBoard();

                    if (myBoard.CheckLoss())
                    {
                        LostGame();
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
                    myBoard.DisplayBoard();
                }
            }
        }

        private void tetrisMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            else if(e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
            else if(e.KeyCode == Keys.Up)
            {
                ClockwiseRotating();
            }
            else if(e.KeyCode == Keys.Down)
            {
                CounterClockwiseRotating();
            }
            else if (e.KeyCode == Keys.Space)
            {
                IncreaseSpeed();
            }
            else if(e.KeyCode == Keys.Home)
            {
                AdvanceLevel();
            }
            else if(e.Control == true)
            {
                if(e.KeyCode == Keys.P)
                {
                    PauseGame();
                }
                else if(e.KeyCode == Keys.R)
                {
                    ResumeGame();
                }
            }
        }

        private void tetrisMenu_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                DecreaseSpeed();
            }
        }

        private void IncreaseSpeed()
        {
            if(currentGameStatus == PlayStatus.Game)
            {
                if(TheTimer.Interval > GameConstants.maximumFastFallingSpeed)
                {
                    TheTimer.Interval = (int)GameConstants.maximumFastFallingSpeed;
                    speedIncreased = true;
                }
                else
                {
                    if(currentFallingSpeed > GameConstants.maximumFastFallingSpeed)
                    {
                        increasedSpeed = (int)GameConstants.maximumFastFallingSpeed;
                    }
                    else
                    {
                        increasedSpeed = currentFallingSpeed;
                    }
                    speedIncreased = true;
                }
            }
            
        }

        private void DecreaseSpeed()
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                speedIncreased = false;
            }
            
        }
    }
}
