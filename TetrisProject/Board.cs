using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    class Board
    {
        private int[,] BoardArray;       //The 2-dimensional array for the state of the board

        private Panel myPanel;           //the panel for the board

        public Board(Panel newPanel)
        {
            BoardArray = new int[GameConstants.rowNumber + GameConstants.pieceGridSizeY, GameConstants.columnNumber];  //all set to zero by default
            myPanel = newPanel;
        }   //constructor

        public void DisplayBoard()         //display the board as a whole
        {
            for(int row = GameConstants.pieceGridSizeY + 1; row <= GameConstants.rowNumber + GameConstants.pieceGridSizeY; row++)
            {
                for(int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    if (BoardArray[row - 1, column - 1] != 0)
                    {
                        BlockGraphics.DisplayBlock(BoardArray[row - 1, column - 1], myPanel, row - GameConstants.pieceGridSizeY, column);
                    }
                }
            }
        }

        public static void ClearDisplayBoard(Panel newPanel)     //clear the board display to let new pieces be on it (should be used first because we need to display the piece as well
        {
            for (int row = GameConstants.pieceGridSizeY + 1; row <= GameConstants.rowNumber + GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    BlockGraphics.DisappearBlock(newPanel, row - GameConstants.pieceGridSizeY, column);
                }
            }
        }

        public int CheckAndClearLines()    //check whether there is any row to be cleared, clear it and return the total line cleared, if return 0 no line is cleared
        {
            int linesCleared = 0;          //the total lines cleared
            bool clear = true;             // whetehr to clear the line
            for(int row = GameConstants.rowNumber + GameConstants.pieceGridSizeY; row >= 1; row--)
            {
                clear = true;
                for (int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    if (BoardArray[row - 1, column - 1] == 0)
                    {
                        clear = false;
                        break;
                    }
                }

               if(clear == true)
               {
                    linesCleared++;       //increase the lines cleared
                    for (int tryRow = row; tryRow > 1; tryRow--)
                    {
                        for (int Trycolumn = 1; Trycolumn <= GameConstants.columnNumber; Trycolumn++)
                        {
                            BoardArray[tryRow - 1, Trycolumn - 1] = BoardArray[tryRow - 1 - 1, Trycolumn -1];  //assign this line to the previous line
                        }
                    }
                    for (int Trycolumn = 1; Trycolumn <= GameConstants.columnNumber; Trycolumn++)
                    {
                        BoardArray[0, Trycolumn - 1] = 0;   //assign the top line to all zero
                    }
                    row++;
                }
            }
            return linesCleared;
        }

        public bool IsPieceInHere(int row, int column)  //naybe used by pieces to check whether it can go somewhere, may reference outside of the bound
        {
            if((row < 1) || (row > GameConstants.rowNumber) ||(column < 1) || (column > GameConstants.columnNumber))
            {
                return false;
            }

            if(BoardArray[row - 1 + GameConstants.pieceGridSizeY, column - 1] != 0)   //may reference out of bound
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddPiece(Piece myPiece)   //add the piece to the board
        {
            for (int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for (int column = 1; column <= GameConstants.pieceGridSizeX; column++)
                {
                    if(myPiece.getGrid(row , column ) == 1)
                    {
                        BoardArray[myPiece.PieceRow + row - 1 - 1, myPiece.PieceColumn + column - GameConstants.pieceGridSizeX - 1] = myPiece.PieceCode;
                    }
                   //don't add if the content is zero
                }
            }
        }

        public bool CheckLoss()   //check whether the player lost this game
        {
            bool youLoss = false;
            for(int row = 1; row <= GameConstants.pieceGridSizeY; row++)
            {
                for(int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    if(BoardArray[row-1,column-1] != 0)
                    {
                        youLoss = true;
                    }
                }
            }
            return youLoss;
        }
    }
}
