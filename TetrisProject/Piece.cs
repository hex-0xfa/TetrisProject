using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    //long piece              red             1
    //left L piece            orange          2
    //right L piece           yellow          3
    //right S piece           green           4
    //left S piece            violet          5
    //square piece            cyan            6
    //Tpiece                  black           7

    abstract class Piece
    {
        public enum RotationStateClockwise             //The rotation status enum
        {
            CW0,                                       //0 clockwise rotation
            CW90,                                      //90 clockwise rotation
            CW180,                                     //180 clockwise rotation
            CW270                                      //270 clockwise rotation
        }

        private int piececode = 0;                     //to hold which piece the block is for calculation and diaplay purpose

        protected int PieceCode
        {
            get
            {
                return piececode;
            }

            private set
            {
                if((value > 0) && (value <= GameConstants.totalPieces))
                {
                    piececode = value;
                }
                else
                {
                    //throw an outside of the valid range exception
                }
            }
        }

        private RotationStateClockwise myRotationSateClockwise;   //The rotation status

        protected RotationStateClockwise MyRotationSateClockwise   //used to provide a property for the child class to see.
        {
            get
            {
                return myRotationSateClockwise;
            }

            private set
            {
                myRotationSateClockwise = value;
            }
        }

        public Piece(RotationStateClockwise newRotationSateClockwise, int newRow, int newColumn, int newPieceCode)
        {
            //Set the rotation of the piece
            MyRotationSateClockwise = newRotationSateClockwise;
            //Set the position of the piece
            pieceRow = newRow;
            pieceColumn = newRow;
            PieceCode = newPieceCode;
            PieceGrid = new int[GameConstants.pieceGridSizeY, GameConstants.pieceGridSizeX];             //set the fill block, needs to be setted specificly in child class
        }

        private int[,] PieceGrid;               //4x4 grid of the piece configration

        protected void setGrid (int row, int column)
        {
            if((row > 0) && (row <= GameConstants.pieceGridSizeY) && (column > 0) && (column <= GameConstants.pieceGridSizeX))
            {
                PieceGrid[row - 1, column - 1] = 1;         //only store 0 or 1 in piece grid
            }
            //throw an outof range exception
        }

        protected int getGrid (int row, int column)
        {
            if ((row > 0) && (row <= GameConstants.pieceGridSizeY) && (column > 0) && (column <= GameConstants.pieceGridSizeX))
            {
                return PieceGrid[row - 1, column - 1];
            }
            //throw an out of range exception
            return 0;
        }

        public void DisplayBoard(Panel DisplayPanel) //Dispaly the piece on board
        {
            for(int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.pieceGridSizeX; column++)
                {
                    if (PieceGrid[row - 1, column - 1] == 1)
                    {
                        BlockGraphics.DisplayBlock(piececode, DisplayPanel, pieceRow - GameConstants.pieceGridSizeY + row - 1, pieceColumn - GameConstants.pieceGridSizeX + 1 + column - 1);
                    }
                    else
                    {
                        //don't display anything if no block is here
                    }
                }
            }
        }

        public void DisappearBoard(Panel DisplayPanel)  //Disappear the piece on board
        {
            for (int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.pieceGridSizeX; column++)
                {
                    if (PieceGrid[row - 1, column - 1] == 1)
                    {
                        BlockGraphics.DisappearBlock(DisplayPanel, pieceRow - GameConstants.pieceGridSizeY + row - 1, pieceColumn - GameConstants.pieceGridSizeX + 1 + column -1);
                    }
                    else
                    {
                        //don't disappear anything if no block is here
                    }
                }
            }
        }

        public void DisplayNext(Panel DisplayPanel)  //Dispaly the piece on the next piece panel
        {
            for (int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.pieceGridSizeX; column++)
                {
                    if (PieceGrid[row - 1, column - 1] == 1)
                    {
                        BlockGraphics.DisplayBlock(piececode, DisplayPanel, row ,column);
                    }
                    else
                    {
                        //don't display anything if no block is here
                    }
                }
            }
        }

        public static void DisappearNext(Panel DisplayPanel) //Clear the next piece panel
        {
            for (int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.pieceGridSizeX; column++)
                {
                    BlockGraphics.DisappearBlock(DisplayPanel, row, column);
                }
            }
        }

        //for the following code, the int returns 0 for successful move, 1 for other pieces or walls blocking (including falling)

        public abstract int MoveLeft(Board myBoard);

        public abstract int MoveRight(Board myBoard);

        public abstract int RotateClockwise(Board myBoard);

        public abstract int RotateCounterClockwise(Board myBoard);

        public abstract int Falling(Board myBoard);

        //Normally we don't need this method, could be implemented in cheat mode
        //public abstract int MovingUp(Board myBoard);

        private int pieceRow;                  //from 1 to 22

        private int pieceColumn;               //from 1 to 13

        public static RotationStateClockwise RandomOrientation ()   //return a random orientation for a piece
        {
            return RotationStateClockwise.CW0;
        }

        public static int RandomPosition()         //return a random position for a piece
        {
            return 0;
        }

        public abstract int returnLeftMostBlock();  //return 1 , 2, 3 or 4, counting from left

        public abstract int returnRightMostBlock(); //return 1, 2, 3 or 4, counting from right

        public abstract int returnBottomMostBlock(); //return 1, 2, 3, or 4, counting from bottom
    }
}
