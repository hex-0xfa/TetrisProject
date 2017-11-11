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
            CW0     =  0,                                       //0 clockwise rotation
            CW90    =  1,                                      //90 clockwise rotation
            CW180   =  2,                                     //180 clockwise rotation
            CW270   =  3                                     //270 clockwise rotation
        }

        private int piececode = 0;                     //to hold which piece the block is for calculation and diaplay purpose

        public int PieceCode
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
            pieceColumn = newColumn;
            //set the code of the piece
            PieceCode = newPieceCode;
        }

        protected int[,] PieceGrid;               //4x4 grid of the piece configration

        protected void setGrid (int row, int column)  //set the piece grid with a specific value
        {
            if((row > 0) && (row <= GameConstants.pieceGridSizeY) && (column > 0) && (column <= GameConstants.pieceGridSizeX))
            {
                PieceGrid[row - 1, column - 1] = 1;         //only store 0 or 1 in piece grid
            }
            //throw an outof range exception
        }

        public int getGrid (int row, int column)   //get a specific value from the piece grid
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

        public int MoveLeft(Board myBoard)
        {
            int statusCode = this.CheckCollision(PieceGrid, myBoard, 0, -1);

            if (statusCode == 1)
            {
                //It can not be moved
                return 1;
            }
            else
            {
                //it can be moved
                pieceColumn = pieceColumn - 1;
                return 0;
            }
        }

        public int MoveRight(Board myBoard)
        {
            int statusCode = this.CheckCollision(PieceGrid, myBoard, 0, 1);

            if (statusCode == 1)
            {
                //It can not be moved
                return 1;
            }
            else
            {
                //it can be moved
                pieceColumn = pieceColumn + 1;
                return 0;
            }
        }

        private static void Rotateclockwise(int [,] myPieceGrid)
        {
            int[,] newPieceGird = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };  //need to be changed for chanoing block size.
            for (int i = 0; i < GameConstants.pieceGridSizeX; i++)
            {
                for (int j = GameConstants.pieceGridSizeY - 1; j >= 0; j--)
                {
                    newPieceGird[j, i] = myPieceGrid[i, GameConstants.pieceGridSizeY - j];
                }
            }
            myPieceGrid = newPieceGird;
        }

        public int CheckRotateClockwise(Board myBoard)
        {
            int[,] myPieceGrid = new int[GameConstants.pieceGridSizeY, GameConstants.pieceGridSizeX];

            for(int i = 0; i < GameConstants.pieceGridSizeY; i++)
            {
                for(int j = 0; j < GameConstants.pieceGridSizeX; j++)
                {
                    myPieceGrid[i, j] = PieceGrid[i, j];
                }
            }

            Piece.Rotateclockwise(myPieceGrid);

            int statusCode = this.CheckCollision(myPieceGrid, myBoard, 0, 0);

            if (statusCode == 1)
            {
                //It can not be rotated this way
                return 1;
            }
            else
            {
                //it can be rotated
                PieceGrid = myPieceGrid;
                return 0;
            }
        }

        public int CheckRotateCounterClockwise(Board myBoard)
        {
            int[,] myPieceGrid = new int[GameConstants.pieceGridSizeY, GameConstants.pieceGridSizeX];

            for (int i = 0; i < GameConstants.pieceGridSizeY; i++)
            {
                for (int j = 0; j < GameConstants.pieceGridSizeX; j++)
                {
                    myPieceGrid[i, j] = PieceGrid[i, j];
                }
            }

            Piece.Rotateclockwise(myPieceGrid);
            Piece.Rotateclockwise(myPieceGrid);
            Piece.Rotateclockwise(myPieceGrid);

            int statusCode = this.CheckCollision(myPieceGrid, myBoard, 0, 0);

            if(statusCode == 1)
            {
                //It can not be rotated this way
                return 1;
            }
            else
            {
                //it can be rotated
                PieceGrid = myPieceGrid;
                return 0;
            }
        }

        private int CheckCollision(int [,] myPieceGrid, Board myBoard, int offsetRow, int offsetColumn)
        {
            for (int row = 0; row < GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 0; column < GameConstants.pieceGridSizeX; column++)
                {
                    if (myPieceGrid[row, column] == 1)
                    {
                        if (((pieceColumn + offsetColumn + column) < (GameConstants.pieceGridSizeX)) ||                //left side wall
                            ((pieceColumn + offsetColumn + column) > (GameConstants.columnNumber + GameConstants.pieceGridSizeX - 1)) ||     //right side wall
                            ((pieceRow + offsetRow + row) > (GameConstants.rowNumber + GameConstants.pieceGridSizeY)) ||             //bottom wall
                             (myBoard.IsPieceInHere(pieceRow + offsetRow + row - GameConstants.pieceGridSizeY, pieceColumn + offsetColumn + column - GameConstants.pieceGridSizeX + 1)))   //other pieces
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

        public int Falling(Board myBoard)
        {
            int statusCode = this.CheckCollision(PieceGrid, myBoard, 1, 0);

            if (statusCode == 1)
            {
                //It can not be moved
                return 1;
                //needs to invoke the addpiece function then
            }
            else
            {
                //it can be moved
                pieceRow = pieceRow + 1;
                return 0;
            }
        }

        //Normally we don't need this method, could be implemented in cheat mode
        //public abstract int MovingUp(Board myBoard);

        private int pieceRow;                  //from 1 to 22

        public int PieceRow
        {
            get
            {
                return pieceRow;
            }
        }

        private int pieceColumn;               //from 1 to 13

        public int PieceColumn
        {
            get
            {
                return pieceColumn;
            }
        }

        public static RotationStateClockwise RandomOrientation ()   //return a random orientation for a piece
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int i = rand.Next(1, 5);  //return 1, 2, 3 or 4
            switch(i)
            {
                case 1:
                    return RotationStateClockwise.CW0;
                case 2:
                    return RotationStateClockwise.CW90;
                case 3:
                    return RotationStateClockwise.CW180;
                case 4:
                    return RotationStateClockwise.CW270;
                default:
                    //throw an exception
                    return RotationStateClockwise.CW0;
            }
        }

        public static int SetPosition()         //return a random position for a piece
        {
            return 0;
        }

        public static int ReturnLeftMostBlock(int[,] myPieceGrid)  //return 1 , 2, 3 or 4, counting from top, left
        {
            int leftMost = 4;
            for (int column = GameConstants.pieceGridSizeX -1; column >= 0; column--)
            {
                for(int row = 0; row < GameConstants.pieceGridSizeY; row++)
                {
                    if(myPieceGrid[row,column] == 1)
                    {
                        leftMost = column + 1;
                    }
                }
            }
            return leftMost;
        }

        public static int ReturnRightMostBlock(int[,] myPieceGrid) //return 1, 2, 3 or 4, counting top, left
        {
            int rightMost = 1;
            for (int column = 0; column < GameConstants.pieceGridSizeX; column++)
            {
                for (int row = 0; row < GameConstants.pieceGridSizeY; row++)
                {
                    if (myPieceGrid[row, column] == 1)
                    {
                        rightMost = column + 1;
                    }
                }
            }
            return rightMost;
        }

        public static int ReturnBottomMostBlock(int[,] myPieceGrid)  //return 1, 2, 3, or 4, counting from top ,left
        {
            int bottomMost = 1;
            for (int row = 0; row < GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 0; column < GameConstants.pieceGridSizeY; column++)
                {
                    if (myPieceGrid[row, column] == 1)
                    {
                        bottomMost = row + 1;
                    }
                }
            }
            return bottomMost;
        }

        public static void SetRandomPieceInitialPosition(ref int row, ref int column, ref Piece.RotationStateClockwise myRotationStateClockwise, ref int pieceCode)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            pieceCode = rand.Next(1, GameConstants.totalPieces + 1);  //return 1, 2, 3, 4, 5, 6, 7
            int[,] myPieceGrid = new int[GameConstants.pieceGridSizeY,GameConstants.pieceGridSizeX];
            switch(pieceCode)
            {
                case 0:
                    //nothing happens
                    break;
                case GameConstants.numberLongPiece:
                    myPieceGrid = LongPiece.ConfigureMap();
                    break;
                case GameConstants.numberLeftLPiece:
                    // Add later
                    break;
                case GameConstants.numberRightLPiece:
                    // Add later
                    break;
                case GameConstants.numberLeftSPiece:
                    // Add later
                    break;
                case GameConstants.numberRightSPiece:
                    // Add later
                    break;
                case GameConstants.numberSquarePiece:
                    // Add later
                    break;
                case GameConstants.numberTPiece:
                    // Add later
                    break;
                default:
                    //return an error here
                    break;
            }

            myRotationStateClockwise = RandomOrientation();

            for(Piece.RotationStateClockwise incrementRotationStateClockwise = RotationStateClockwise.CW0; incrementRotationStateClockwise < myRotationStateClockwise; incrementRotationStateClockwise++)
            {
                Piece.Rotateclockwise(myPieceGrid);
            }

            row = GameConstants.pieceGridSizeY + 1 - Piece.ReturnBottomMostBlock(myPieceGrid);

            int leftMostColumn = GameConstants.pieceGridSizeX + 1 - Piece.ReturnLeftMostBlock(myPieceGrid);

            int rightMostColumn = GameConstants.pieceGridSizeX + GameConstants.columnNumber - Piece.ReturnRightMostBlock(myPieceGrid);

            Random rand2 = new Random(Guid.NewGuid().GetHashCode());
            column = rand2.Next(leftMostColumn, rightMostColumn + 1);  //return 1, 2, 3, 4, 5, 6, 7
        }
    }
}
