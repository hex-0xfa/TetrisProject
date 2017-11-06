using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    //long piece              red
    //left L piece            orange
    //right L piece           yellow
    //right S piece           green
    //left S piece            violet
    //square piece            cyan
    //Tpiece                  black
    abstract  class Piece
    {
        public Piece(int myRotationStatus, int myRow, int myColumn)
        {
            rotationStatus = myRotationStatus;
            row = myRow;
            column = myColumn;
        }

        protected int[,] PieceGrid;

        public abstract void Display();

        public abstract void Dispeear();

        private int row;

        private int column;

        private int rotationStatus;
    }
}
