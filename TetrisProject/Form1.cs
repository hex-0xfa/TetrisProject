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
    public partial class TetrisForm : Form
    {

        private Piece currentPiece;               //used to point to the current piece

        private Piece nextPiece;                  //used to point to the next piece

        private Board myBoard;                    //used to do the calculations and provide abstaction with the board class

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

        public TetrisForm()            //constructor
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
            }
            else if(myPlayStatus == PlayStatus.Game)
            {
                currentGameStatus = PlayStatus.Game;
                playPauseButton.Text = "Pause";
            }
            else
            {
                currentGameStatus = PlayStatus.Pause;
                playPauseButton.Text = "Resume";
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveLeft(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.MoveRight(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.CheckRotateClockwise(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentGameStatus == PlayStatus.Game)
            {
                currentPiece.DisappearBoard(panelBoard);

                int status = currentPiece.CheckRotateCounterClockwise(myBoard);

                currentPiece.DisplayBoard(panelBoard);

                myBoard.DisplayBoard();
            }
        }

        public void QuitProgram()  //All three
        {

        }      

        public void ExitGame()  // Game  Pause
        {
            ChangeGameStatus(PlayStatus.Inactive);
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
            ChangeGameStatus(PlayStatus.Game);
            ResetScore();
            ResetLines();
            RestartLevel();
            //start the timers
        }

        public void LostGame()
        {
            test.Visible = true;   //tell the user they have lost

            ChangeGameStatus(PlayStatus.Inactive);
        }

        public void PauseGame()    //Game
        {
            ChangeGameStatus(PlayStatus.Pause);
            //set the timer
        }

        public void ResumeGame()   //Pasue
        {
            ChangeGameStatus(PlayStatus.Game);
            //set the timer
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
                return;   //may add way to handle the mistake
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
            currentLevel = currentLevel + 1;
            UpdateDisplayLevel();
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
    }
}
