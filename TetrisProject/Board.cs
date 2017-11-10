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
        private int[,] BoardArray;

        private Panel myPanel;

        public Board(Panel newPanel)
        {
            BoardArray = new int[GameConstants.rowNumber, GameConstants.columnNumber];  //all set to zero by default
            myPanel = newPanel;
        }

        public void DisplayBoard()         //display the board as a whole
        {
            for(int row = 1; row <= GameConstants.rowNumber; row++)
            {
                for(int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    BlockGraphics.DisplayBlock(BoardArray[row - 1, column - 1], myPanel, row, column);
                }
            }
        }

        public static void ClearDisplayBoard(Panel newPanel)     //clear the board display to let new pieces be on it (should be used first because we need to display the piece as well
        {
            for (int row = 1; row <= GameConstants.rowNumber; row++)
            {
                for (int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    BlockGraphics.DisappearBlock(newPanel, row, column);
                }
            }
        }

        public int CheckAndClearLines()    //check whether there is any row to be cleared, clear it and return the total line cleared, if return 0 no line is cleared
        {
            int linesCleared = 0;          //the total lines cleared
            bool clear = true;             // whetehr to clear the line
            for(int row = GameConstants.rowNumber; row >= 1; row--)
            {
                clear = true;
                for(int column = 1; column <= GameConstants.columnNumber; column++)
                {
                    if (BoardArray[row - 1, column - 1] == 0)
                    {
                        clear = false;
                    }

                    if(clear == true)
                    {
                        linesCleared++;       //increase the lines cleared
                        for (int tryRow = row; tryRow >= 1; tryRow--)
                        {
                            for (int Trycolumn = 1; column <= GameConstants.columnNumber; column++)
                            {
                                BoardArray[tryRow - 1, Trycolumn] = BoardArray[tryRow - 1 - 1, Trycolumn -1];  //assign this line to the previous line
                            }
                        }
                        for (int Trycolumn = 1; column <= GameConstants.columnNumber; column++)
                        {
                            BoardArray[0, Trycolumn - 1] = 0;   //assign the top line to all zero
                        }
                    }
                }
            }
            return linesCleared;
        }

        public bool IsPieceInHere(int row, int column)  //naybe used by pieces to check whether it can go somewhere
        {
            if(BoardArray[row - 1, column - 1] != 0)   //may reference out of bound
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddPiece(Piece myPiece)   //probably need to intergrate the check and clear
        {
            
        }
    }
}
