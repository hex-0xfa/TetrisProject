using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    /// <summary>
    /// Child class of piece that implement square piece
    /// </summary>
    class SquarePiece : Piece
    {
        public SquarePiece(RotationStateClockwise newRotationSateClockwise, int newRow, int newColumn) //constructor
         : base (newRotationSateClockwise, newRow, newColumn, GameConstants.numberSquarePiece)
        {
            PieceGrid = SquarePiece.ConfigureMap();
            for (Piece.RotationStateClockwise incrementRotationStateClockwise = RotationStateClockwise.CW0; incrementRotationStateClockwise < MyRotationSateClockwise; incrementRotationStateClockwise++)
            {
                PieceGrid = Piece.RotateClockwise(PieceGrid);
            }
        }

        public static int[,] ConfigureMap()        //the bit map for square piece
        {
            int[,] newPieceGrid = { { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
            return newPieceGrid;
        }
    }
}
