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
        public static void DisplayBlock(Color blockColor, Panel DislayPanel, int row, int column)  //display a block accroding to the direction given, check for valid input.
        {
            if(DislayPanel.Name == "panelBoard")
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
            else if(DislayPanel.Name == "nextBlockPanel")
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
            Graphics myblock = new Graphics();
            Rectangle marginBlock = new Rectangle((column - 1) * VisualConstants.blockSizeX, (row - 1) * VisualConstants.blockSizeY, VisualConstants.blockSizeX, VisualConstants.blockSizeY);
            SolidBrush myBrushMar = new SolidBrush(VisualConstants.marginColor);
            myBlock.FillRectangle(myBrushMar, marginBlock);
            Rectangle innerBlock = new Rectangle((column - 1) * sizeX + margin, (row - 1) * sizeY + margin, sizeX - 2*margin, sizeY - 2*margin);
            SolidBrush myBrushCol = new SolidBrush(blockColor);
            myBlock.FillRectangle(myBrushCol, innerBlock);
        }
        public void DisappearBlock()
        {
            Rectangle OverrideBlock = new Rectangle((column - 1) * sizeX, (row - 1) * sizeY, sizeX, sizeY);
            SolidBrush myBrush = new SolidBrush(VisualConstants.boardBackground);
            myBlock.FillRectangle(myBrush, OverrideBlock);
        }
    }
}
