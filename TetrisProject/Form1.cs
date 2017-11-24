﻿using System;
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
        DateTime startTime;                       //used to pause the timer              

        int elapsedTime;                             //used to pasue the timer

        private Piece currentPiece;               //used to point to the current piece

        private Piece nextPiece;                  //used to point to the next piece

        private Board myBoard;                    //used to do the calculations and provide abstaction with the board class

        private int currentFallingSpeed;          //keep the current normal falling speed

        private int increasedSpeed;               //The speed after increase

        private bool upKeyPressed;

        private bool downKeyPressed;

        private bool leftKeyPressed;

        private bool rightKeyPressed;

        private bool spaceKeyPressed;

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

        private int upKeyPressedNumber;

        private int downKeyPressedNumber;

        private int leftKeyPressedNumber;

        private int rightKeyPressedNumber;

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

            ClearAllKeys();
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
                ClearAllKeys();
            }
            else if(myPlayStatus == PlayStatus.Game)
            {
                currentGameStatus = PlayStatus.Game;
                playPauseButton.Text = "Pause";
                TheTimer.Enabled = true;
                startTime = DateTime.Now;
            }
            else
            {
                currentGameStatus = PlayStatus.Pause;
                playPauseButton.Text = "Resume";
                TheTimer.Enabled = false;
                ClearAllKeys();
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

            //could redisplay the panel
        }

        public void StartNewGame()  //Inactive Game Pause
        {
            test.Visible = false;
            ResetScore();
            ResetLines();
            RestartLevel();
            Board.ClearDisplayBoard(panelBoard);
            myBoard = new Board(panelBoard);
            myBoard.DisplayBoard();
            currentPiece = Piece.GenerateRandomPieceOnTop();
            currentPiece.DisplayBoard(panelBoard);
            nextPiece = Piece.GenerateRandomPieceOnTop();
            Piece.DisappearNext(nextBlockPanel);
            nextPiece.DisplayNext(nextBlockPanel);
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
                elapsedTime = (DateTime.Now.Subtract(startTime)).Milliseconds;
                ChangeGameStatus(PlayStatus.Pause);
            }
        }

        public void ResumeGame()   //Pasue
        {
            if (currentGameStatus == PlayStatus.Pause)
            {
                TheTimer.Interval = TheTimer.Interval - elapsedTime;
                ChangeGameStatus(PlayStatus.Game);
            }
        }

        public void LoadGame()     //Inactive Pause
        {

        }

        public void SaveGame()     //Pause
        {

        }

        public void AddLinesCleared(int lines)
        {
            if ((lines < 1) || (lines > GameConstants.pieceGridSizeY))
            {
                return;   //not correct line number
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
            if(spaceKeyPressed == true)
            {
                TheTimer.Interval = increasedSpeed;
            }
            else
            {
                TheTimer.Interval = currentFallingSpeed;
            }
            elapsedTime = 0;
            startTime = DateTime.Now;
            Falling();
        }

        //could add different audio effect for successful or unsuccessful move

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
                    ClearAllKeys();
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
            if (e.KeyCode == Keys.Left)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    if (leftKeyPressed == false)
                    {
                        MoveLeft();
                        TurnOnLeftKey();
                    }
                }
            }
            else if(e.KeyCode == Keys.Right)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    if (rightKeyPressed == false)
                    {
                        MoveRight();
                        TurnOnRightKey();
                    }
                }
            }
            else if(e.KeyCode == Keys.Up)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    if (upKeyPressed == false)
                    {
                        ClockwiseRotating();
                        TurnOnUpKey();
                    }
                }
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    if (downKeyPressed == false)
                    {
                        CounterClockwiseRotating();
                        TurnOnDownKey();
                    }
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    if (spaceKeyPressed == false)
                    {
                        IncreaseSpeed();
                    }
                }
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
            if (e.KeyCode == Keys.Left)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    TurnOffLeftKey();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    TurnOffRightKey();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    TurnOffUpKey();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    TurnOffDownKey();
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (currentGameStatus == PlayStatus.Game)
                {
                    DecreaseSpeed();
                }
            }
        }

        private void ClearAllKeys()
        {
            TurnOffLeftKey();
            TurnOffRightKey();
            TurnOffUpKey();
            TurnOffDownKey();
            DecreaseSpeed();
        }

        private void TurnOnLeftKey()
        {
            leftKeyPressed = true;
            LeftKeyTimer.Interval = GetKeyRefreshRate();
            LeftKeyTimer.Enabled = true;
        }

        private void TurnOnRightKey()
        {
            rightKeyPressed = true;
            RightKeyTimer.Interval = GetKeyRefreshRate();
            RightKeyTimer.Enabled = true;

        }

        private void TurnOnUpKey()
        {
            upKeyPressed = true;
            UpKeyTimer.Interval = GetKeyRefreshRate();
            UpKeyTimer.Enabled = true;
        }

        private void TurnOnDownKey()
        {
            downKeyPressed = true;
            DownKeyTimer.Interval = GetKeyRefreshRate();
            DownKeyTimer.Enabled = true;
        }

        private void IncreaseSpeed()
        {
            spaceKeyPressed = true;

            if (currentGameStatus == PlayStatus.Game)
            {
                if (TheTimer.Interval > GameConstants.maximumFastFallingSpeed)
                {
                    TheTimer.Interval = (int)GameConstants.maximumFastFallingSpeed;

                    increasedSpeed = (int)GameConstants.maximumFastFallingSpeed;
                }
                else
                {
                    if (currentFallingSpeed > GameConstants.maximumFastFallingSpeed)
                    {
                        increasedSpeed = (int)GameConstants.maximumFastFallingSpeed;
                    }
                    else
                    {
                        increasedSpeed = currentFallingSpeed;
                    }
                }
            }
        }

        private void TurnOffLeftKey()
        {
            leftKeyPressed = false;
            LeftKeyTimer.Enabled = false;
            leftKeyPressedNumber = 0;
        }

        private void TurnOffRightKey()
        {
            rightKeyPressed = false;
            RightKeyTimer.Enabled = false;
            rightKeyPressedNumber = 0;
        }

        private void TurnOffUpKey()
        {
            upKeyPressed = false;
            UpKeyTimer.Enabled = false;
            upKeyPressedNumber = 0;
        }

        private void TurnOffDownKey()
        {
            downKeyPressed = false;
            DownKeyTimer.Enabled = false;
            downKeyPressedNumber = 0;
        }

        private void DecreaseSpeed()
        {
            spaceKeyPressed = false;
        }

        private int GetKeyRefreshRate()
        {
            if (currentFallingSpeed > (GameConstants.baseKeyRefreshRate * GameConstants.MinOperationPerFalling))
            {
                return GameConstants.baseKeyRefreshRate;
            }
            else
            {
                return (currentFallingSpeed / GameConstants.MinOperationPerFalling);
            }
        }

        private void UpKeyTimer_Tick(object sender, EventArgs e)
        {
            if(upKeyPressedNumber < GameConstants.keyHoldTimeMultiplier)
            {
            }
            else
            {
                ClockwiseRotating();
            }
            upKeyPressedNumber++;
        }

        private void DownKeyTimer_Tick(object sender, EventArgs e)
        {
            if(downKeyPressedNumber < GameConstants.keyHoldTimeMultiplier)
            {                
            }
            else
            {
                CounterClockwiseRotating();
            }
            downKeyPressedNumber++;
        }

        private void LeftKeyTimer_Tick(object sender, EventArgs e)
        {
            if(leftKeyPressedNumber < GameConstants.keyHoldTimeMultiplier)
            {
            }
            else
            {
                MoveLeft();
            }
            leftKeyPressedNumber++;
        }

        private void RightKeyTimer_Tick(object sender, EventArgs e)
        {
            if (rightKeyPressedNumber < GameConstants.keyHoldTimeMultiplier)
            {
            }
            else
            {
                MoveRight();
            }
            rightKeyPressedNumber++;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PauseGame();
            MessageBox.Show("press up key for clockwise rotation\n" +
                            "press down key for counterclockwise rotation\n" +
                            "press left key for moving left\n" +
                            "press right key for mving right\n" +
                            "press space for speed up falling\n" +
                            "press play button to start or restart play\n" +
                            "press pause button to pause game\n" +
                            "press resume button to resume game\n" +
                            "按上箭头顺时针旋转\n" +
                            "按下箭头逆时针旋转\n" +
                            "按左箭头向左移动\n" +
                            "按右箭头向右移动\n" +
                            "按空格键加速下降\n" +
                            "按play按钮开始或重新开始游戏\n" +
                            "按pasue按钮暂停游戏\n" +
                            "按resume按钮继续游戏\n");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PauseGame();
            MessageBox.Show("Tetirs for C# windows form\n" +
                            "俄罗斯方块\n\n" +
                            "developed by Henry Chu\n" +
                            "由 Henry Chu 开发\n\n" +
                            "email : chuchenxi_1997@163.com"
                            );

        }
    }
}
