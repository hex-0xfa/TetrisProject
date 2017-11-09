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

    abstract class Piece
    {
        public enum RotationStateClockwise
        {
            CW0,                              //0 rotation
            CW90,                             //90 rotation
            CW180,                            //180 rotation
            CW270                             //270 rotation
        }

        protected RotationStateClockwise MyRotationSateClockwise { get; }

        public Piece(RotationStateClockwise newRotationSateClockwise, int newRow, int newColumn)
        {
            //Set the rotation of the piece
            MyRotationSateClockwise = newRotationSateClockwise;
            //Set the position of the piece
            row = newRow;
            column = newRow;
            //initialize the piece grid in the child class
        }


        private int[,] PieceGrid;               //4x4

        public abstract void Display();

        public abstract void Dispeear();

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
