using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    static class GameConstants
    {

        //Defined by Chenxi Chu

        public const int rowNumber = 18;          //The number of rows for the game

        public const int columnNumber = 10;       //The number of columns for the game

        public const double baseInterval = 500.00;  //The base interval of falling in ms

        public const double maximumFastFallingSpeed = 50.00;  //the maximum fast falling speed

        public const int basePointsForRow = 100;  //The basis point earned for one row cleared

        //equation for calculating points: currentLevel * ( basePointsForRow * rowCleared + bonusPointsForExtraRow * ( rowCleared - 1 )

        public const int bonusPointsForExtraRow = 50;  //Bonus Points for extra row cleared

        public static int ModifiedBasePoints(int lines)  //The modified base points baesd on lines cleared
        {
            if((lines < 1) || (lines > pieceGridSizeY))
            {
                return 0;
            }
            else
            {
                return basePointsForRow * lines + bonusPointsForExtraRow * (lines - 1);
            }
        }

        public const int nextLevelLines = 10;   //The Lines needed to be cleared to enter the next level

        public const double speedIncrease = 0.8;  //The increase of falling speed of the block for each level advanced

        public const int pieceGridSizeX = 4;    //The size of piece grid X

        public const int pieceGridSizeY = 4;    //The size of piece grid Y

        public const int nextBlockPanelRow = pieceGridSizeX;  //The row number of next block panel

        public const int nextBlockPanelColumn = pieceGridSizeY;   //The column number of next block panel

        public const int totalPieces = 7;         //The total kind of pieces

        //The number of pieces

        public const int numberLongPiece = 1;

        public const int numberLeftLPiece = 2;

        /* x
        *  xxx
        */

        public const int numberRightLPiece = 3;

        /*   x
        *  xxx
        */

        public const int numberRightSPiece = 4;

        /*  xx
        *  xx
        */

        public const int numberLeftSPiece = 5;

        /* xx
        *   xx
        */

        public const int numberSquarePiece = 6;

        public const int numberTPiece = 7;


        public static int validPiecePositionX = pieceGridSizeX - 1 + columnNumber;

        public static int validPiecePostionY = pieceGridSizeY + rowNumber;

        public const int baseKeyRefreshRate = 50;

        public const int MinOperationPerFalling = 8;

        public const int keyHoldTimeMultiplier = 1;

        public const int RotationHoldTimeMultiplier = 2;
    }
}
