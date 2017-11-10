using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisProject
{
    public static class BlockGraphics
    {
        public static void DisplayBlock(int blcokNumber, Panel DisplayPanel, int row, int column)  //display a block accroding to the direction given, check for valid input.
        {
            Color blockColor = VisualConstants.boardBackground;

            switch (blcokNumber)            //needs to be changed if the number system is changed, set the color according to the code of each pieces
            {
                case GameConstants.numberLongPiece:
                    blockColor = VisualConstants.colorLongPiece;
                    break;
                case GameConstants.numberLeftLPiece:
                    blockColor = VisualConstants.colorLeftLPiece;
                    break;
                case GameConstants.numberRightLPiece:
                    blockColor = VisualConstants.colorRightLPiece;
                    break;
                case GameConstants.numberLeftSPiece:
                    blockColor = VisualConstants.colorLeftSPiece;
                    break;
                case GameConstants.numberRightSPiece:
                    blockColor = VisualConstants.colorRightSPiece;
                    break;
                case GameConstants.numberSquarePiece:
                    blockColor = VisualConstants.colorSquarePiece;
                    break;
                case GameConstants.numberTPiece:
                    blockColor = VisualConstants.colorTPiece;
                    break;
                default:
                    //return an error here
                    break;

            }
            if(DisplayPanel.Name == "panelBoard")
            {
                if((row > 0) && (row <= GameConstants.rowNumber) && (column > 0) && (column <= GameConstants.columnNumber))
                {
                    //The block is inside
                }
                else
                {
                    return;
                    //display outside of the size
                }
            }
            else if(DisplayPanel.Name == "nextBlockPanel")
            {
                if ((row > 0) && (row <= GameConstants.nextBlockPanelRow) && (column > 0) && (column <= GameConstants.nextBlockPanelColumn))
                {
                    //The block is inside
                }
                else
                {
                    return;
                    //display outside of the size
                }
            }
            else
            {
                return;
                //the block is displayed in positon which it can't be, could implement a way to notify the user.
            }
            Graphics myBlock = DisplayPanel.CreateGraphics();
            Rectangle marginBlock = new Rectangle((column - 1) * VisualConstants.blockSizeX, (row - 1) * VisualConstants.blockSizeY, VisualConstants.blockSizeX, VisualConstants.blockSizeY);
            SolidBrush myBrushMar = new SolidBrush(VisualConstants.marginColor);
            myBlock.FillRectangle(myBrushMar, marginBlock);                //dispaly a big margin color block
            Rectangle innerBlock = new Rectangle((column - 1) * VisualConstants.blockSizeX + VisualConstants.blockMargin, (row - 1) * VisualConstants.blockSizeY + VisualConstants.blockMargin, VisualConstants.blockSizeX - 2* VisualConstants.blockMargin, VisualConstants.blockSizeY - 2* VisualConstants.blockMargin);
            SolidBrush myBrushCol = new SolidBrush(blockColor);
            myBlock.FillRectangle(myBrushCol, innerBlock);                 //display a smaller true color block
        }

        public static void DisappearBlock(Panel DisplayPanel, int row, int column) //clear a block accroding to the direction given, check for valid input.
        {
            if (DisplayPanel.Name == "panelBoard")
            {
                if ((row > 0) && (row <= GameConstants.rowNumber) && (column > 0) && (column <= GameConstants.columnNumber))
                {
                    //The block is inside
                }
                else
                {
                    return;
                    //display outside of the size
                }
            }
            else if (DisplayPanel.Name == "nextBlockPanel")
            {
                if ((row > 0) && (row <= GameConstants.nextBlockPanelRow) && (column > 0) && (column <= GameConstants.nextBlockPanelColumn))
                {
                    //The block is inside
                }
                else
                {
                    return;
                    //display outside of the size
                }
            }
            else
            {
                return;
                //the block is displayed in panels which it can't be, could implement a way to notify the user.
            }
            Graphics myBlock = DisplayPanel.CreateGraphics();
            Rectangle OverrideBlock = new Rectangle((column - 1) * VisualConstants.blockSizeX, (row - 1) * VisualConstants.blockSizeY, VisualConstants.blockSizeX, VisualConstants.blockSizeY);
            SolidBrush myBrush = new SolidBrush(VisualConstants.boardBackground);
            myBlock.FillRectangle(myBrush, OverrideBlock);   //fill the block with the background color, which makes it disappear.
        }
    }
}
