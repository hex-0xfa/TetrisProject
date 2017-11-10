using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisProject
{
    static class VisualConstants
    {
        //Layout Pixels
      
        public static int YBorderSize = 47;                //The y size of the border

        public static int XBorderSize = 18;                //The x size of the border

        public static int MenuSzie = 28;                   //The menu y size of the border

        public static int blockSizeX = 35;                 //The horizontal size of the block

        public static int blockSizeY = 35;                 //The vertical size of the block

        public static int blockMargin = 2;                 //The size of the margin

        public static int boardSizeX = GameConstants.columnNumber * blockSizeX;  //The X size of the board

        public static int boardSizeY = GameConstants.rowNumber * blockSizeY;   //The Y size of the board

        public static int upperMargin = 20;                //The upper margin between board and menu

        public static int lowerMargin = 20;                //The lower margin between board and border

        public static int leftMargin = 20;                 //The left margin between border and board

        public static int middleMargin = 20;                 //The mindle margin between border and board

        public static int rightMargin = 20;                 //The right margin between border and board

        public static int displayPanelWidth = 250;          //The width of the dispaly menu

        public static int WindowSizeX = leftMargin + boardSizeX + middleMargin + displayPanelWidth + rightMargin;               //The horizontal size of the window

        public static int WindowsSizeY = upperMargin + boardSizeY + lowerMargin + MenuSzie;              //The vertical size of the window

        public static int LabelX = VisualConstants.boardSizeX + VisualConstants.leftMargin + VisualConstants.middleMargin;     //The left side of the label

        public static int LabelWidth = 250;                 //The length of all labels

        public static int LabelHeight = 40;                 //The Height of all labels except play button


        //Colors

        public static Color formColor = Color.Black;

        public static Color boardBackground = Color.Blue;

        public static Color marginColor = Color.White;

        //Fonts

        public static string DefaultFont = "Comic Sans MS";   //The default font to use when displaying texts. (unused for now)

        //Colors of the Pieces

        public static Color colorLongPiece = Color.Red;

        public static Color colorLeftLPiece = Color.Orange;

         /* x
         *  xxx
         */

        public static Color colorRightLPiece = Color.Yellow;

        /*   x
        *  xxx
        */

        public static Color colorRightSPiece = Color.Green;

        /*  xx
        *  xx
        */

        public static Color colorLeftSPiece = Color.Violet;

        /* xx
        *   xx
        */

        public static Color colorSquarePiece = Color.Cyan;

        public static Color colorTPiece = Color.Black;

    }
}
