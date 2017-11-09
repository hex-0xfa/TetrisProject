using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisProject
{
    class Block
    {
        public Block(System.Drawing.Color myColor, Panel newPanel, int newRow, int newColumn)
        {
            blockColor = myColor;
            sizeX = VisualConstants.blockSizeX;
            sizeY = VisualConstants.blockSizeY;
            margin = VisualConstants.blockMargin;
            myPanel = newPanel;
            row = newRow;
            column = newColumn;
            myBlock = myPanel.CreateGraphics();
        }

        private Panel myPanel;

        private Color blockColor;

        private int margin;

        private int sizeX;

        private int sizeY;

        private int row;

        private int column;

        private Graphics myBlock;

        public void DisplayBlock()
        {
            Rectangle marginBlock = new Rectangle((column - 1) * sizeX, (row - 1) * sizeY, sizeX, sizeY);
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
