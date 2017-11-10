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
            CW0,                                       //0 rotation
            CW90,                                      //90 rotation
            CW180,                                     //180 rotation
            CW270                                      //270 rotation
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
            row = newRow;
            column = newRow;
            PieceCode = newPieceCode;
            //initialize the piece grid in the child class
        }


        private int[,] PieceGrid;               //4x4 grid of the piece configration

        protected void setGrid (int row, int column)
        {
            if((row > 0) && (row <= GameConstants.pieceGridSizeY) && (column > 0) && (column <= GameConstants.pieceGridSizeX))
            {
                PieceGrid[row - 1, column - 1] = PieceCode;
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

        public abstract void Display(Panel DisplayPanel);         

        public abstract void Disappear(Panel DisplayPanel);

        //for the following code, the int returns 0 for successful move, 1 for other pieces or walls blocking (including falling)

        public abstract int MoveLeft(Board myBoard);

        public abstract int MoveRight(Board myBoard);

        public abstract int RotateClockwise(Board myBoard);

        public abstract int RotateCounterClockwise(Board myBoard);

        public abstract int Falling(Board myBoard);

        //Normally we don't need this method, could be implemented in cheat mode
        //public abstract int MovingUp(Board myBoard);

        private int row;

        private int column;


    }
}
